namespace EBooks.Core.Entities.User;

public class UserDbModel : DbModel
{
    public string Email { get; init; }
    public string FullName { get; init; }
}