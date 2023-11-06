using HowTo.Entities.Interactive.Base;

namespace HowTo.Entities.Interactive.ChoiceOfAnswers;

public class LogChoiceOfAnswerDto : InteractiveLogBase
{
    public string AnswersJsonBoolArray { get; set; }
    public string SuccessAnswersJsonBoolArray { get; set; }
}