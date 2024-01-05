namespace EBooks.Authorization;

public class User
{
    public User(uint id, string name, UserRole userRole)
    {
        Id = id;
        Name = name;
        UserRole = userRole;
    }
    public uint Id { get; set; }
    public string Name { get; set; }
    public UserRole UserRole { get; set; }
}