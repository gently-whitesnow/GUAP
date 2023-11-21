namespace EBooks.Core.Entities.Book;

public class BookSummaryView
{
    public uint Id { get; init; }
    public string Title { get; init; }
    public string Author { get; init; }
    public int Count { get; init; }
    public int AvailableCount { get; set; }
}