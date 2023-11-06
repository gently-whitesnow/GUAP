using HowTo.Entities.Interactive.Base;

namespace HowTo.Entities.Interactive.ChoiceOfAnswers;

public class ChoiceOfAnswerDto: InteractiveBase
{
    public string QuestionsJsonStringArray { get; set; }
    public string AnswersJsonBoolArray { get; set; }
}