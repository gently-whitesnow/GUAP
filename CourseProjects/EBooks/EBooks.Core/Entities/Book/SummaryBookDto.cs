namespace EBooks.Core.Entities.Book;

public class SummaryBookDto
{
    public string SearchQuery { get; init; }
    public bool IsAvailable { get; init; }
    public uint Skip { get; init; }
    public uint Take { get; init; }
}