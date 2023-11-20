namespace EBooks.Core.Entities.Book;

public class SummaryBookDto
{
    public string SearchQuery { get; init; }
    public bool IsAvailable { get; init; }
    public int Skip { get; init; }
    public int Take { get; init; }
}