namespace HowTo.Entities.Views;

public class ViewDto
{
    public Guid EntityId { get; set; }
    public HashSet<Guid> UserId { get; set; }
}