using System.Net;
using EBooks.BO.Services;
using EBooks.Core.Entities.Options;
using EBooks.DA;
using EBooks.DA.Repositories;

namespace EBooks;

public static class Startup
{
    private const string DefaultCorsPolicyName = "DefaultCorsPolicyName";
    
    public static WebApplicationBuilder SetUpHost(this WebApplicationBuilder builder)
    {
        builder.Host.UseContentRoot(Directory.GetCurrentDirectory());

        builder.WebHost.UseKestrel(options =>
        {
            options.Listen(IPAddress.Any, 1234);
            options.AllowSynchronousIO = true;
        }).ConfigureLogging((context, loggingBuilder) =>
        {
            loggingBuilder
                .ClearProviders()
                .AddConsole();
        }).UseDefaultServiceProvider((context, options) =>
        {
            var environmentName = context.HostingEnvironment.EnvironmentName;
            options.ValidateScopes = context.HostingEnvironment.IsDevelopment() || environmentName == "dev";
            options.ValidateOnBuild = context.HostingEnvironment.IsDevelopment() || environmentName is "dev" or "staging";
        });

        var env = builder.Environment;

        builder.Configuration
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables()
            .Build();
        
        return builder;
    }
    
    public static IServiceCollection WithRepositories(this IServiceCollection services)
    {
        services.AddSingleton<BooksRepository>();
        services.AddSingleton<ReservationsRepository>();
        services.AddSingleton<UsersRepository>();
        return services;
    }
    
    public static IServiceCollection WithServices(this IServiceCollection services)
    {
        services.AddSingleton<BooksService>();
        services.AddSingleton<ReservationsService>();
        return services;
    }
    
    public static IServiceCollection WithHelpers(this IServiceCollection services)
    {
        services.AddSingleton<FileSystemHelper>();
        return services;
    }
    public static IServiceCollection WithCors(this IServiceCollection services)
    {
        services.AddCors(options =>
            options.AddPolicy(DefaultCorsPolicyName,
                corsPolicyBuilder => corsPolicyBuilder
                    .WithOrigins("http://127.0.0.1:5173", "http://localhost:5173") // TODO
                    .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                    .AllowAnyHeader()
                    .AllowCredentials()));
        return services;
    }
    
    public static IServiceCollection WithOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FileSystemOptions>(configuration.GetSection("FileSystemOptions"));
        return services;
    }
    
    public static IServiceCollection WithPackages(this IServiceCollection services)
    {
        services.AddHealthChecks();
        services.AddSingleton<ExceptionHandlerMiddleware>();

        services
            .AddControllers(options =>
            {
                options.SuppressInputFormatterBuffering = true;
                options.SuppressOutputFormatterBuffering = true;
            })
            .AddControllersAsServices();

        return services;
    }

    public static void UseAppAsync(this WebApplication app)
    {
        const string healthCheckRoute = "/_internal/healthcheck";
        
        app.UseRouting();
        app.UseCors(DefaultCorsPolicyName);
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks(healthCheckRoute);
        });
    }
}