namespace EBooks.Core.Entities.Book;

public class BookDbModel : DbModel
{
    public string Title { get; init; }
    public string Description { get; init; }
    public string Author { get; init; }
    public ushort Count { get; init; }
}