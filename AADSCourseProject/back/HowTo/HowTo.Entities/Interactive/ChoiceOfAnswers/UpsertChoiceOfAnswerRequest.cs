namespace HowTo.Entities.Interactive.ChoiceOfAnswers;

public class UpsertChoiceOfAnswerRequest
{
    public string[] Questions { get; set; }
    public bool[] Answers { get; set; }
}