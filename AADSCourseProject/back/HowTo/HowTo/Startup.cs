using System.Net;
using ATI.Services.Common.Behaviors;
using ATI.Services.Common.Extensions;
using ATI.Services.Common.Initializers;
using ATI.Services.Common.Metrics;
using HowTo.DataAccess.Managers;
using HowTo.DataAccess.Repositories;
using HowTo.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
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

        return services;
    }
    public static IServiceCollection WithAdapters(this IServiceCollection services)
    {
        return services;
    }
    
    public static IServiceCollection WithMangers(this IServiceCollection services)
    {
        services.AddSingleton<ArticleManager>();
        services.AddSingleton<CourseManager>();
        services.AddSingleton<SummaryManager>();
        services.AddSingleton<ViewManager>();
        return services;
    }
    public static IServiceCollection WithRepositories(this IServiceCollection services)
    {
        services.AddSingleton<ArticleRepository>();
        services.AddSingleton<CourseRepository>();
        services.AddSingleton<ViewRepository>();
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