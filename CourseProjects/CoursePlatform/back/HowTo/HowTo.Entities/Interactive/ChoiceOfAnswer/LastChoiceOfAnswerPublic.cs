using HowTo.Entities.Interactive.Base;
using HowTo.Entities.Interactive.CheckList;
using HowTo.Entities.Interactive.ChoiceOfAnswers;
using Newtonsoft.Json;

namespace HowTo.Entities.Interactive.ChoiceOfAnswer;

public class LastChoiceOfAnswerPublic : LastInteractiveBase
{
    public LastChoiceOfAnswerPublic(LastChoiceOfAnswerDto dto)
    {
        Id = dto.Id;
        ArticleId = dto.ArticleId;
        CourseId = dto.CourseId;
        UserId = dto.UserId;
        Answers = JsonConvert.DeserializeObject<bool[]>(dto.AnswersJsonBoolArray);
        SuccessAnswers = JsonConvert.DeserializeObject<bool[]>(dto.SuccessAnswersJsonBoolArray);
    }
    public bool[] Answers { get; set; }
    public bool[]? SuccessAnswers { get; set; }
}