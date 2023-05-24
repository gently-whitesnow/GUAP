using System;

namespace HowTo.Entities;

public class User
{
    public User(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
}