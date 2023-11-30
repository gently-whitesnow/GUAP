using Flow;
using Flow.StandardOperationError;

namespace EBooks.Core.Entities;

public static class Errors
{
    public static readonly Error BookNotFoundError = 
        new NotFoundError("book_not_found", "Книга не найдена");
    
    public static readonly Error AlreadyReserveByUserError = 
        new BadRequestError("already_reserve_by_user", "Книга уже зарезервирована пользователем");
    
    public static readonly Error NotAvailableBooksError = 
        new BadRequestError("not_available_books", "В наличии отсутствуют экзмепляры этой книги");
    
    public static readonly Error NotOwnerOfReservationError = 
        new ForbiddenError("not_owner_reservation", "Пользователь не числится зарезервировавшим эту книгу");
}