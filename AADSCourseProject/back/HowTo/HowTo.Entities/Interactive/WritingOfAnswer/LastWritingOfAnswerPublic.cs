using HowTo.Entities.Interactive.Base;

namespace HowTo.Entities.Interactive.WritingOfAnswer;

public class LastWritingOfAnswerPublic : LastInteractiveBase
{
    public LastWritingOfAnswerPublic(LastWritingOfAnswerDto dto)
    {
        Id = dto.Id;
        ArticleId = dto.ArticleId;
        CourseId = dto.CourseId;
        UserId = dto.UserId;
        Answer = dto.Answer;
        Success = dto.Success;
    }

    public string Answer { get; set; }
    public bool Success { get; set; }
}