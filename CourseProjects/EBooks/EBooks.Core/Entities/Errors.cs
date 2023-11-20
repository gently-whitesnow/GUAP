using Flow.StandardOperationError;

namespace EBooks.Core.Entities;

public static class Errors
{
    public static readonly OperationError BookNotFoundOperationError = 
        new NotFoundOperationError("book_not_found", "Книга не найдена");
}