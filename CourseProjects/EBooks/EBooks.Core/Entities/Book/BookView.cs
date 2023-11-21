using EBooks.Core.Entities.User;

namespace EBooks.Core.Entities.Book;

public class BookView
{
    public uint Id { get; set; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Author { get; init; }
    public int Count { get; init; }
    public DateTimeOffset AddDate { get; set; }

    public UsersReservationView Reservations { get; set; }
}