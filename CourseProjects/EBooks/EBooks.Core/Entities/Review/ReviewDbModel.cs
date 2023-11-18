namespace EBooks.Core.Entities.Review;

public class ReviewDbModel
{
    public uint Id { get; init; }
    public uint BookId { get; init; }
    public uint UserId { get; init; }
    public string Content { get; init; }
    public DateTimeOffset AddDate { get; init; }
}