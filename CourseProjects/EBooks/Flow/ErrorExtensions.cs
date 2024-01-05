using System.Net;
using Flow.StandardOperationError;

namespace Flow;

public static class ErrorExtensions
{
    public static int ExtractStatusCode(this Error error) => (int)(error switch
    {
        BadRequestError => HttpStatusCode.BadRequest,
        ForbiddenError => HttpStatusCode.Forbidden,
        NotFoundError => HttpStatusCode.NotFound,
        _ => throw new NotImplementedException("Данный тип ошибки не определен")
    });
}
