using System;
using System.ComponentModel.DataAnnotations;

namespace HowTo.Entities.Views;

public class UserGuid
{
    public UserGuid(Guid userId)
    {
        UserId = userId;
    }

    [Key]
    public int Id { get; set; }
    public Guid UserId { get; set; }
}