namespace EBooks.Core.Entities.Book;

public class BooksSearchView
{
    public BookSummaryView[] Books { get; init; }
    public int TotalCount { get; init; }
}