using System.ComponentModel.DataAnnotations;

namespace HowTo.Entities.Interactive.WritingOfAnswer;

public class UpsertWritingOfAnswerRequest
{
    [Required]
    public string Answer { get; set; }
}