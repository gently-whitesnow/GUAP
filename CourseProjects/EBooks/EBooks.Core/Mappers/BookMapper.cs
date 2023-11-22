using EBooks.Core.Entities.Book;
using EBooks.Core.Entities.Reservation;
using EBooks.Core.Entities.User;

namespace EBooks.Core.Mappers;

public static class BookMapper
{
    public static BookView ToBookView(this BookDbModel bookDbModel, UsersReservationView reservations)
    {
        return new BookView
        {
            Id = bookDbModel.Id,
            Title = bookDbModel.Title,
            Description = bookDbModel.Description,
            Author = bookDbModel.Author,
            AddDate = bookDbModel.AddDate,
            Count = bookDbModel.Count,
            Reservations = reservations
        };
    }
    
    public static BookSummaryView ToBookSummaryView(this BookDbModel bookDbModel)
    {
        return new BookSummaryView
        {
            Id = bookDbModel.Id,
            Title = bookDbModel.Title,
            Author = bookDbModel.Author,
            Count = bookDbModel.Count
        };
    }
    
    public static BookDbModel ToBookDbModel(this BookUpsertDto bookUpsertDto)
    {
        return new BookDbModel
        {
            Id = bookUpsertDto.Id,
            Title = bookUpsertDto.Title,
            Description = bookUpsertDto.Description,
            Author = bookUpsertDto.Author,
            Count = bookUpsertDto.Count
        };
    }
}