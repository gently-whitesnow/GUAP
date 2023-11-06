using HowTo.Entities.Interactive.Base;

namespace HowTo.Entities.Interactive.WritingOfAnswer;

public class WritingOfAnswerPublic : InteractiveBase
{
    public WritingOfAnswerPublic(WritingOfAnswerDto dto, bool isAuthor = true)
    {
        Id = dto.Id;
        ArticleId = dto.ArticleId;
        CourseId = dto.CourseId;
        Description = dto.Description;
        Answer = isAuthor ? dto.Answer : null;
    }

    public string? Answer { get; set; }
}