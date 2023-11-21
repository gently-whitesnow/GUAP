namespace EBooks.Core.Entities.Book;

public class BookUpsertDto
{
    public uint Id { get; set; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Author { get; init; }
    public ushort Count { get; init; }
}