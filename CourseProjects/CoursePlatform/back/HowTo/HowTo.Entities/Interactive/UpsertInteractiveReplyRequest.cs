using System.ComponentModel.DataAnnotations;
using HowTo.Entities.Interactive.Base;
using HowTo.Entities.Interactive.CheckList;
using HowTo.Entities.Interactive.ChoiceOfAnswers;
using HowTo.Entities.Interactive.ProgramWriting;
using HowTo.Entities.Interactive.WritingOfAnswer;

namespace HowTo.Entities.Interactive;

public class UpsertInteractiveReplyRequest
{
    [Required] 
    public int InteractiveId { get; set; }
    [Required] 
    public int CourseId { get; set; }
    [Required] 
    public int ArticleId { get; set; }
    
    public UpsertReplyCheckListRequest ReplyCheckList { get; set; }
    public UpsertReplyAnswerChoiceRequest ReplyAnswerChoice { get; set; }
    public UpsertReplyProgramWritingRequest ReplyProgramWriting { get; set; }
    public UpsertReplyWritingOfAnswerRequest ReplyWritingOfAnswer { get; set; }
}