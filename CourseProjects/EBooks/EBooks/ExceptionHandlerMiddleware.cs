using System.Net;

namespace EBooks;

public class ExceptionHandlerMiddleware : IMiddleware
{
    private const string CatchedOnMiddlewareError = "Обработана ошибка на мидлваре";
    private static readonly object InternalServerError =
        new { Error = "internal_server_error", Reason = "Внутренняя ошибка сервера" };

    private readonly ILogger _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, CatchedOnMiddlewareError);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(InternalServerError);
        }
    }
}
