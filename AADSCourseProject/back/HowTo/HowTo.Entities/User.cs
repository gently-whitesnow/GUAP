using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HowTo.Entities;

[ComplexType]
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}