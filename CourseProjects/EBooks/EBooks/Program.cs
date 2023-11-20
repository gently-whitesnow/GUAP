using EBooks;

var builder = WebApplication.CreateBuilder(args)
    .SetUpHost();

var services = builder.Services;

services.AddControllers(options =>
    {
        options.SuppressInputFormatterBuffering = true;
        options.SuppressOutputFormatterBuffering = true;
    })
    .AddControllersAsServices();

services.WithRepositories()
    .WithServices()
    .WithPackages();

var app = builder.Build();

app.UseAppAsync();

app.Run();