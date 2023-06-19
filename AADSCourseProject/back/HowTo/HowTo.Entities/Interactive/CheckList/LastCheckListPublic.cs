using HowTo.Entities.Interactive.Base;
using Newtonsoft.Json;

namespace HowTo.Entities.Interactive.CheckList;

public class LastCheckListPublic : LastInteractiveBase
{
    public LastCheckListPublic(LastCheckListDto dto)
    {
        Id = dto.Id;
        ArticleId = dto.ArticleId;
        CourseId = dto.CourseId;
        UserId = dto.UserId;
        Clauses = JsonConvert.DeserializeObject<bool[]>(dto.CheckedClausesJsonBoolArray);
    }
    public bool[] Clauses { get; set; }
}