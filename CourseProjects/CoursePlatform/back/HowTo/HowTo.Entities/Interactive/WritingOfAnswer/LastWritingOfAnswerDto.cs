using HowTo.Entities.Interactive.Base;

namespace HowTo.Entities.Interactive.WritingOfAnswer;

public class LastWritingOfAnswerDto : LastInteractiveBase
{
    public string Answer { get; set; }
    public bool Success { get; set; }
}