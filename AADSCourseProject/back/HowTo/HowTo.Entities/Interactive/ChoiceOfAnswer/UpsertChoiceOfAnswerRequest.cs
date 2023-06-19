using System.ComponentModel.DataAnnotations;

namespace HowTo.Entities.Interactive.ChoiceOfAnswers;

public class UpsertChoiceOfAnswerRequest
{
    [Required]
    public string[] Questions { get; set; }
    [Required]
    public bool[] Answers { get; set; }
}