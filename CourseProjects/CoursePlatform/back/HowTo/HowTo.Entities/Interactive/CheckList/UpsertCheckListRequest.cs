using System.ComponentModel.DataAnnotations;

namespace HowTo.Entities.Interactive.CheckList;

public class UpsertCheckListRequest
{
    [Required] public string[] Clauses { get; set; }
}