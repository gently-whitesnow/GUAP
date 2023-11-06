using System.Security.Principal;

namespace HowTo.Entities.Extensions;

public class UserIdentity : IIdentity
{
    public UserIdentity(User user)
    {
        User = user;
    }
    public User User { get; set; }
    public string? AuthenticationType { get; }
    public bool IsAuthenticated { get; }
    public string? Name { get; }
}