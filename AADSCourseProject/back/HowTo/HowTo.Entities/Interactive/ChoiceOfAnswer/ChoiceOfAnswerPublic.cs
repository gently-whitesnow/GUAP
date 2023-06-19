using HowTo.Entities.Interactive.Base;
using HowTo.Entities.Interactive.ChoiceOfAnswers;
using Newtonsoft.Json;

namespace HowTo.Entities.Interactive.ChoiceOfAnswer;

public class ChoiceOfAnswerPublic : InteractiveBase
{
    public ChoiceOfAnswerPublic(ChoiceOfAnswerDto dto, bool isAuthor = true)
    {
        Id = dto.Id;
        ArticleId = dto.ArticleId;
        CourseId = dto.CourseId;
        Description = dto.Description;
        Questions = JsonConvert.DeserializeObject<string[]>(dto.QuestionsJsonStringArray);
        Answers = isAuthor ? JsonConvert.DeserializeObject<bool[]>(dto.AnswersJsonBoolArray) : null;
    }

    public string[] Questions { get; set; }
    public bool[] Answers { get; set; }
}