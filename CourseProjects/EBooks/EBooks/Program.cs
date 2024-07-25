using EBooks;
using EBooks.Extensions;

var builder = WebApplication.CreateBuilder(args)
    .SetUpHost();

var configuration = builder.Configuration;

var services = builder.Services;

services.AddControllers(options =>
    {
        options.SuppressInputFormatterBuffering = true;
        options.SuppressOutputFormatterBuffering = true;
    })
    .AddControllersAsServices()
    .AddJsonOptions(
        options => { 
            options.JsonSerializerOptions.PropertyNamingPolicy = 
                SnakeCaseNamingPolicy.Instance;
        });

services.WithRepositories()
    .WithServices()
    .WithHelpers()
    .WithPackages()
    .WithCors()
    .WithOptions(configuration);

var app = builder.Build();

app.UseAppAsync();

app.Run();