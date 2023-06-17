using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HowTo.Entities.Interactive.Base;

public class InteractiveLogBase : IHaveId
{
    [Key] [JsonIgnore] 
    public int Id { get; set; }
    public int InteractiveId { get; set; }
    public Guid UserId { get; set; }
    public DateTimeOffset LogDate { get; set; }
}