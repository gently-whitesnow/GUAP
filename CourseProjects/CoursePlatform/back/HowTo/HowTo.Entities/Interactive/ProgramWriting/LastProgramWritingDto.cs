using HowTo.Entities.Interactive.Base;

namespace HowTo.Entities.Interactive.ProgramWriting;

public class LastProgramWritingDto : LastInteractiveBase
{
    public string Code { get; set; }
    public bool Success { get; set; }
}