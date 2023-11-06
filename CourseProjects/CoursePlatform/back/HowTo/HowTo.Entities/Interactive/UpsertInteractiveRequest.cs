using HowTo.Entities.Interactive.Base;
using HowTo.Entities.Interactive.CheckList;
using HowTo.Entities.Interactive.ChoiceOfAnswers;
using HowTo.Entities.Interactive.ProgramWriting;
using HowTo.Entities.Interactive.WritingOfAnswer;

namespace HowTo.Entities.Interactive;

public class UpsertInteractiveRequest
{
    public int? Id { get; set; }
    public int ArticleId { get; set; }
    public int CourseId { get; set; }
    public string Description { get; set; }
    public UpsertCheckListRequest UpsertCheckListRequest { get; set; }
    public UpsertChoiceOfAnswerRequest UpsertChoiceOfAnswerRequest { get; set; }
    public UpsertProgramWritingRequest UpsertProgramWritingRequest { get; set; }
    public UpsertWritingOfAnswerRequest UpsertWritingOfAnswerRequest { get; set; }
}