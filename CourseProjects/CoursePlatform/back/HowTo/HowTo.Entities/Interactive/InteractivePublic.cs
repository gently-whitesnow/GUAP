using HowTo.Entities.Interactive.CheckList;
using HowTo.Entities.Interactive.ChoiceOfAnswer;
using HowTo.Entities.Interactive.ChoiceOfAnswers;
using HowTo.Entities.Interactive.ProgramWriting;
using HowTo.Entities.Interactive.WritingOfAnswer;

namespace HowTo.Entities.Interactive;

public class InteractivePublic
{
    public InteractivePublic(CheckListPublic[] checkList,
        ChoiceOfAnswerPublic[] choiceOfAnswer,
        ProgramWritingPublic[] programWriting,
        WritingOfAnswerPublic[] writingOfAnswer)
    {
        CheckList = checkList;
        ChoiceOfAnswer = choiceOfAnswer;
        ProgramWriting = programWriting;
        WritingOfAnswer = writingOfAnswer;
    }
    public CheckListPublic[] CheckList { get; set; }
    public ChoiceOfAnswerPublic[] ChoiceOfAnswer { get; set; }
    public ProgramWritingPublic[] ProgramWriting { get; set; }
    public WritingOfAnswerPublic[] WritingOfAnswer { get; set; }
}