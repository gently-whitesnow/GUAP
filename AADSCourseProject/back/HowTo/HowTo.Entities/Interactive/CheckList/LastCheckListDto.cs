using HowTo.Entities.Interactive.Base;

namespace HowTo.Entities.Interactive.CheckList;

public class LastCheckListDto : LastInteractiveBase
{
    public string CheckedClausesJsonBoolArray { get; set; }
}