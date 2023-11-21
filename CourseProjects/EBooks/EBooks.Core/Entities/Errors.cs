using Flow.StandardOperationError;

namespace EBooks.Core.Entities;

public static class Errors
{
    public static readonly OperationError BookNotFoundOperationError = 
        new NotFoundOperationError("book_not_found", "Книга не найдена");
    
    public static readonly OperationError AlreadyReserveByUserError = 
        new BadRequestOperationError("already_reserve_by_user", "Книга уже зарезервирована пользователем");
    
    public static readonly OperationError NotAvailableBooksError = 
        new BadRequestOperationError("not_available_books", "В наличии отсутствуют экзмепляры этой книги");
    
    public static readonly OperationError NotOwnerOfReservationError = 
        new ForbiddenOperationError("not_owner_reservation", "Пользователь не числится зарезервировавшим эту книгу");
}