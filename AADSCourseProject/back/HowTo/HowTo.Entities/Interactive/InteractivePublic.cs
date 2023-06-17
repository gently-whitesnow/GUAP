using HowTo.Entities.Interactive.ChoiceOfAnswers;

namespace HowTo.Entities.Interactive;

public class InteractivePublic
{
    public InteractivePublic(CheckList.CheckListDto[] checkList, ChoiceOfAnswerDto[] choiceOfAnswer, ProgramWriting.ProgramWritingDto[] programWriting, WritingOfAnswer.WritingOfAnswerDto[] writingOfAnswer)
    {
        CheckList = checkList;
        ChoiceOfAnswer = choiceOfAnswer;
        ProgramWriting = programWriting;
        WritingOfAnswer = writingOfAnswer;
    }
    public CheckList.CheckListDto[] CheckList { get; set; }
    public ChoiceOfAnswerDto[] ChoiceOfAnswer { get; set; }
    public ProgramWriting.ProgramWritingDto[] ProgramWriting { get; set; }
    public WritingOfAnswer.WritingOfAnswerDto[] WritingOfAnswer { get; set; }
}