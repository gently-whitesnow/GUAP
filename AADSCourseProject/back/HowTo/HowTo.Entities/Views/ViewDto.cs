using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HowTo.Entities.Views;

public class ViewDto
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public List<UserIdEntity> Viewers { get; set; }
}