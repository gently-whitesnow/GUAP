using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HowTo.Entities.Views;

[ComplexType]
public class UserIdEntity
{
    public UserIdEntity(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}