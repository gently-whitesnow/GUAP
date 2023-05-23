using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HowTo.Entities.Contributor;

public class ContributorEntity
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
}