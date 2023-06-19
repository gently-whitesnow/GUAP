using HowTo.Entities.Interactive.CheckList;
using HowTo.Entities.Interactive.ChoiceOfAnswer;
using HowTo.Entities.Interactive.ChoiceOfAnswers;
using HowTo.Entities.Interactive.ProgramWriting;
using HowTo.Entities.Interactive.WritingOfAnswer;

namespace HowTo.Entities.Interactive;

public class InteractiveByIdPublic
{
    public InteractiveByIdPublic(CheckListDto dto)
    {
        CheckList = new CheckListPublic(dto);
    }

    public InteractiveByIdPublic(ChoiceOfAnswerDto dto)
    {
        ChoiceOfAnswer = new ChoiceOfAnswerPublic(dto);
    }

    public InteractiveByIdPublic(ProgramWritingDto dto)
    {
        ProgramWriting = new ProgramWritingPublic(dto);
    }

    public InteractiveByIdPublic(WritingOfAnswerDto dto)
    {
        WritingOfAnswer = new WritingOfAnswerPublic(dto);
    }
    public CheckListPublic CheckList { get; init; }
    public ChoiceOfAnswerPublic ChoiceOfAnswer { get; init; }
    public ProgramWritingPublic ProgramWriting { get; init; }
    public WritingOfAnswerPublic WritingOfAnswer { get; init; }
}