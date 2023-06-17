using HowTo.Entities.Interactive.CheckList;
using HowTo.Entities.Interactive.ChoiceOfAnswers;
using HowTo.Entities.Interactive.ProgramWriting;
using HowTo.Entities.Interactive.WritingOfAnswer;

namespace HowTo.Entities.Interactive;

public class LastInteractivePublic
{
    public LastInteractivePublic(LastCheckListDto dto)
    {
        LastCheckList = dto;
    }

    public LastInteractivePublic(LastChoiceOfAnswerDto dto)
    {
        LastChoiceOfAnswer = dto;
    }

    public LastInteractivePublic(LastProgramWritingDto dto)
    {
        LastProgramWriting = dto;
    }

    public LastInteractivePublic(LastWritingOfAnswerDto dto)
    {
        LastWritingOfAnswer = dto;
    }

    public LastInteractivePublic(LastCheckListDto lastCheckList, LastChoiceOfAnswerDto lastChoiceOfAnswer, LastProgramWritingDto lastProgramWriting, LastWritingOfAnswerDto lastWritingOfAnswer)
    {
        LastCheckList = lastCheckList;
        LastChoiceOfAnswer = lastChoiceOfAnswer;
        LastProgramWriting = lastProgramWriting;
        LastWritingOfAnswer = lastWritingOfAnswer;
    }
    public LastCheckListDto LastCheckList { get; set; }
    public LastChoiceOfAnswerDto LastChoiceOfAnswer { get; set; }
    public LastProgramWritingDto LastProgramWriting { get; set; }
    public LastWritingOfAnswerDto LastWritingOfAnswer { get; set; }
}