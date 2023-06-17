using HowTo.Entities.Interactive.CheckList;
using HowTo.Entities.Interactive.ChoiceOfAnswers;
using HowTo.Entities.Interactive.ProgramWriting;
using HowTo.Entities.Interactive.WritingOfAnswer;

namespace HowTo.Entities.Interactive;

public class InteractiveByIdPublic
{
    public InteractiveByIdPublic(CheckListDto dto)
    {
        CheckList = dto;
    }

    public InteractiveByIdPublic(ChoiceOfAnswerDto dto)
    {
        ChoiceOfAnswer = dto;
    }

    public InteractiveByIdPublic(ProgramWritingDto dto)
    {
        ProgramWriting = dto;
    }

    public InteractiveByIdPublic(WritingOfAnswerDto dto)
    {
        WritingOfAnswer = dto;
    }
    public CheckListDto CheckList { get; set; }
    public ChoiceOfAnswerDto ChoiceOfAnswer { get; set; }
    public ProgramWritingDto ProgramWriting { get; set; }
    public WritingOfAnswerDto WritingOfAnswer { get; set; }
}