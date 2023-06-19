using HowTo.Entities.Interactive.CheckList;
using HowTo.Entities.Interactive.ChoiceOfAnswer;
using HowTo.Entities.Interactive.ChoiceOfAnswers;
using HowTo.Entities.Interactive.ProgramWriting;
using HowTo.Entities.Interactive.WritingOfAnswer;

namespace HowTo.Entities.Interactive;

public class LastInteractivePublic
{
    public LastInteractivePublic(LastCheckListDto dto)
    {
        LastCheckList = new LastCheckListPublic(dto);
    }

    public LastInteractivePublic(LastChoiceOfAnswerDto dto)
    {
        LastChoiceOfAnswer = new LastChoiceOfAnswerPublic(dto);
    }

    public LastInteractivePublic(LastProgramWritingDto dto)
    {
        LastProgramWriting = new LastProgramWritingPublic(dto);
    }

    public LastInteractivePublic(LastWritingOfAnswerDto dto)
    {
        LastWritingOfAnswer = new LastWritingOfAnswerPublic(dto);
    }

    public LastInteractivePublic(LastCheckListPublic? lastCheckList,
        LastChoiceOfAnswerPublic? lastChoiceOfAnswer,
        LastProgramWritingPublic? lastProgramWriting,
        LastWritingOfAnswerPublic? lastWritingOfAnswer)
    {
        LastCheckList = lastCheckList;
        LastChoiceOfAnswer = lastChoiceOfAnswer;
        LastProgramWriting = lastProgramWriting;
        LastWritingOfAnswer = lastWritingOfAnswer;
    }
    public LastCheckListPublic? LastCheckList { get; init; }
    public LastChoiceOfAnswerPublic? LastChoiceOfAnswer { get; init; }
    public LastProgramWritingPublic? LastProgramWriting { get; init; }
    public LastWritingOfAnswerPublic? LastWritingOfAnswer { get; init; }
}