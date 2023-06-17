using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using ATI.Services.Common.Extensions;
using ATI.Services.Common.Initializers;
using ATI.Services.Common.Metrics;
using HowTo.DataAccess.Helpers;
using HowTo.DataAccess.Managers;
using HowTo.DataAccess.Repositories;
using HowTo.Entities;
using HowTo.Entities.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ConfigurationManager = ATI.Services.Common.Behaviors.ConfigurationManager;

namespace HowTo;

public static class Startup
{
    #region WebApplicationBuilder
    public static WebApplicationBuilder SetUpHost(this WebApplicationBuilder builder)
    {
        builder.Host.UseContentRoot(Directory.GetCurrentDirectory());

        builder.WebHost.UseKestrel(options =>
        {
            options.Listen(IPAddress.Any, ConfigurationManager.GetApplicationPort());
            options.AllowSynchronousIO = true;
        }).ConfigureLogging((context, loggingBuilder) =>
        {
            loggingBuilder
                .ClearProviders()
                .AddConsole();
        }).UseDefaultServiceProvider((context, options) =>
        {
            var environmentName = context.HostingEnvironment.EnvironmentName;
            //Scoped services aren't directly or indirectly resolved from the root service provider.
            //Scoped services aren't directly or indirectly injected into singletons.
            options.ValidateScopes = context.HostingEnvironment.IsDevelopment() || environmentName == "dev";
            //Validate DI tree on startup    
            options.ValidateOnBuild = context.HostingEnvironment.IsDevelopment() || environmentName is "dev" or "staging";
        });

        var env = builder.Environment;

        builder.Configuration
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        ConfigurationManager.ConfigurationRoot = builder.Configuration;

        return builder;
    }

    #endregion
    
    #region ServiceCollection
    public static IServiceCollection WithOptions(this IServiceCollection services)
    {
        services.ConfigureByName<DbSettings>();
        services.ConfigureByName<FileSystemOptions>();
        return services;
    }
    public static IServiceCollection WithServices(this IServiceCollection services)
    {
        services.AddHealthChecks();
        services.AddSingleton(new JsonSerializer
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        });
        
        services
            .AddControllers(options =>
            {
                options.SuppressInputFormatterBuffering = true;
                options.SuppressOutputFormatterBuffering = true;
            })
            .AddNewtonsoftJson(
                options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };
                });
        
        CommonBehavior.SetSerializer(new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy { ProcessDictionaryKeys = false }
            }
        });

        return services;
    }
    public static IServiceCollection WithAdapters(this IServiceCollection services)
    {
        return services;
    }
    
    public static IServiceCollection WithHelpers(this IServiceCollection services)
    {
        services.AddSingleton<FileSystemHelper>();
        return services;
    }
    
    public static IServiceCollection WithMangers(this IServiceCollection services)
    {
        services.AddSingleton<ArticleManager>();
        services.AddSingleton<CourseManager>();
        services.AddSingleton<SummaryManager>();
        services.AddSingleton<ViewManager>();
        services.AddSingleton<UserInfoManager>();
        services.AddSingleton<InteractiveManager>();
        return services;
    }
    public static IServiceCollection WithRepositories(this IServiceCollection services)
    {
        services.AddSingleton<ApplicationContext>();
        services.AddSingleton<ArticleRepository>();
        services.AddSingleton<CourseRepository>();
        services.AddSingleton<ViewRepository>();
        services.AddSingleton<UserInfoRepository>();
        services.AddSingleton<InteractiveRepository>();
        return services;
    }

    public static IServiceCollection WithExtensionsInfrastructure(this IServiceCollection services)
    {
        // todo for LocalCache
        services.AddInitializers();
        return services;
    }

    #endregion
    
    #region WebApplication
    public static Task UseAppAsync(this WebApplication app)
    {
        const string healthCheckRoute = "/_internal/healthcheck";
        
        app.UseRouting();
        app.UseCors(CommonBehavior.AllowAllOriginsCorsPolicyName);
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapMetricsCollection();
            endpoints.MapHealthChecks(healthCheckRoute);
        });
        
        // ReSharper disable once ConvertToLocalFunction
        var notify = () => Console.WriteLine(@"Application Port - " + ConfigurationManager.GetApplicationPort());

        var services = app.Services;
        
        using var scope = services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        // Необходимо для созданий миграций, при поднятии сервиса
        var context = serviceProvider.GetRequiredService<ApplicationContext>();
        context.Database.Migrate();

        var initTask = app.Services.GetRequiredService<StartupInitializer>()
            .InitializeAsync().ContinueWith(_ => notify());
        
        return initTask;
    }

    #endregion
}