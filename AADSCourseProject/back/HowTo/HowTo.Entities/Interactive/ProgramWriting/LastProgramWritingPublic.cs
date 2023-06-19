using HowTo.Entities.Interactive.Base;

namespace HowTo.Entities.Interactive.ProgramWriting;

public class LastProgramWritingPublic : LastInteractiveBase
{
    public LastProgramWritingPublic(LastProgramWritingDto dto)
    {
        Id = dto.Id;
        ArticleId = dto.ArticleId;
        CourseId = dto.CourseId;
        UserId = dto.UserId;
        Code = dto.Code;
        Success = dto.Success;
    }
    public string Code { get; set; }
    public bool Success { get; set; }
}