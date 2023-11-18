namespace EBooks.Core.Entities.Book;

public class BookDbModel
{
    public uint Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Author { get; init; }
    public DateTimeOffset AddDate { get; init; }
    public ushort Count { get; init; }
}