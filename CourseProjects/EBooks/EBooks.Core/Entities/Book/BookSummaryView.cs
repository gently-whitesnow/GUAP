namespace EBooks.Core.Entities.Book;

public class BookSummaryView
{
    public string Title { get; init; }
    public string Author { get; init; }
    public ushort Count { get; init; }
    public ushort AvailableCount { get; init; }
}