using System.ComponentModel.DataAnnotations;

namespace HowTo.Entities.Interactive.ProgramWriting;

public class UpsertProgramWritingRequest
{
    [Required]
    public string Code { get; set; }
    [Required]
    public string Output { get; set; }
}