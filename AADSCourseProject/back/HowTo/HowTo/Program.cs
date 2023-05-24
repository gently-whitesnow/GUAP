using ATI.Services.Common.Behaviors;
using ATI.Services.Common.Serializers.SystemTextJsonSerialization;
using HowTo;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args)
    .SetUpHost();

var services = builder.Services;

services.AddCors(options =>
    options.AddPolicy(CommonBehavior.AllowAllOriginsCorsPolicyName,
        corsPolicyBuilder => corsPolicyBuilder
            .WithOrigins("http://localhost:3000") // TODO
            .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
            .AllowAnyHeader()
            .AllowCredentials()));

services.AddControllers(options =>
    {
        options.SuppressInputFormatterBuffering = true;
        options.SuppressOutputFormatterBuffering = true;
    })
    .AddControllersAsServices()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
    });

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