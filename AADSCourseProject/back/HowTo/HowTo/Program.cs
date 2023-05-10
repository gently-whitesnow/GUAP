using ATI.Services.Common.Behaviors;
using HowTo;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args)
    .SetUpHost();

var services = builder.Services;

services.AddCors(options =>
    options.AddPolicy(CommonBehavior.AllowAllOriginsCorsPolicyName,
        corsPolicyBuilder => corsPolicyBuilder.WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
            .AllowAnyOrigin()
            .AllowAnyHeader()));

services.AddControllers(options =>
    {
        options.SuppressInputFormatterBuffering = true;
        options.SuppressOutputFormatterBuffering = true;
    })
    .AddControllersAsServices()
    .AddNewtonsoftJson(
        options =>
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy
                {
                    ProcessDictionaryKeys = true,
                    OverrideSpecifiedNames = true
                }
            };
        }
    );

services.WithOptions()
    .WithServices()
    .WithMangers()
    .WithAdapters()
    .WithHelpers()
    .WithRepositories()
    .WithExtensionsInfrastructure();

var app = builder.Build();

await app.UseAppAsync();

app.Run();