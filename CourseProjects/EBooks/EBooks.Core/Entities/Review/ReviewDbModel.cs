namespace EBooks.Core.Entities.Review;

public class ReviewDbModel : DbModel
{
    public uint BookId { get; init; }
    public uint UserId { get; init; }
    public string Content { get; init; }
}