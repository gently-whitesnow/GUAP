namespace EBooks.Core.Entities.Book;

public class BookDbModel : DbModel
{
    public string Title { get; init; }
    public string Description { get; init; }
    public string Author { get; init; }
    public int Count { get; init; }
}