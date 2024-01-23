namespace EBooks.Core.Entities.Book;

public class YourBookView
{
    public uint ReservationId { get; init; }
    public string Title { get; init; }
    public string Author { get; init; }
    
    public IEnumerable<byte[]> Files { get; set; }
}