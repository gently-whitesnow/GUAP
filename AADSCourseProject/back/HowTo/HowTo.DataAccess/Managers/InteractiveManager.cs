using System;
using System.Linq;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using ATI.Services.Common.Extensions;
using ATI.Services.Common.Extensions.OperationResult;
using HowTo.DataAccess.Repositories;
using HowTo.Entities;
using HowTo.Entities.Interactive;
using HowTo.Entities.Interactive.CheckList;
using HowTo.Entities.Interactive.ChoiceOfAnswers;
using HowTo.Entities.Interactive.ProgramWriting;
using HowTo.Entities.Interactive.WritingOfAnswer;
using Newtonsoft.Json;

namespace HowTo.DataAccess.Managers;

public class InteractiveManager
{
    private readonly InteractiveRepository _interactiveRepository;
    
    private readonly ArticleRepository _articleRepository;

    public InteractiveManager(
        InteractiveRepository interactiveRepository,
        ArticleRepository articleRepository)
    {
        _interactiveRepository = interactiveRepository;
        _articleRepository = articleRepository;
    }

    public async Task<OperationResult<InteractiveByIdPublic>> UpsertInteractiveAsync(UpsertInteractiveRequest request)
    {
        var articleOperation = await _articleRepository.GetArticleByIdAsync(request.CourseId, request.ArticleId);
        if (!articleOperation.Success)
            return new(articleOperation);

        dynamic interactiveOperation = request.Interactive switch
        {
            Interactive.CheckList => await _interactiveRepository.UpsertInteractiveAsync(request, () => new CheckListDto
                {
                    ArticleId = request.ArticleId,
                    CourseId = request.CourseId,
                    Description = request.Description,
                    ClausesJsonStringArray = JsonConvert.SerializeObject(request.UpsertCheckListRequest.Clauses),
                },
                dto =>
                {
                    dto.Description = request.Description;
                    dto.ClausesJsonStringArray = JsonConvert.SerializeObject(request.UpsertCheckListRequest.Clauses);
                }),
            
            
            Interactive.ChoiceOfAnswer => await _interactiveRepository.UpsertInteractiveAsync(request, () =>
                    new ChoiceOfAnswerDto
                    {
                        ArticleId = request.ArticleId,
                        CourseId = request.CourseId,

                        Description = request.Description,
                        QuestionsJsonStringArray =
                            JsonConvert.SerializeObject(request.UpsertChoiceOfAnswerRequest.Questions),
                        AnswersJsonBoolArray = JsonConvert.SerializeObject(request.UpsertChoiceOfAnswerRequest.Answers),
                    },
                dto =>
                {
                    dto.Description = request.Description;
                    dto.QuestionsJsonStringArray =
                        JsonConvert.SerializeObject(request.UpsertChoiceOfAnswerRequest.Questions);
                    dto.AnswersJsonBoolArray =
                        JsonConvert.SerializeObject(request.UpsertChoiceOfAnswerRequest.Answers);
                }),
            
            
            Interactive.ProgramWriting => await _interactiveRepository.UpsertInteractiveAsync(request, () =>
                    new ProgramWritingDto
                    {
                        ArticleId = request.ArticleId,
                        CourseId = request.CourseId,

                        Description = request.Description,
                        Code = request.UpsertProgramWritingRequest.Code,
                        Output = request.UpsertProgramWritingRequest.Output
                    },
                dto =>
                {
                    dto.Description = request.Description;
                    dto.Code = request.UpsertProgramWritingRequest.Code;
                    dto.Output = request.UpsertProgramWritingRequest.Output;
                }),
            
            
            Interactive.WritingOfAnswer => await _interactiveRepository.UpsertInteractiveAsync(request, () =>
                    new WritingOfAnswerDto
                    {
                        ArticleId = request.ArticleId,
                        CourseId = request.CourseId,

                        Description = request.Description,
                        Answer = request.UpsertWritingOfAnswerRequest.Answer
                    },
                dto =>
                {
                    dto.Description = request.Description;
                    dto.Answer = request.UpsertWritingOfAnswerRequest.Answer;
                }),
            _ => new OperationResult<InteractivePublic>(new ArgumentException("Invalid interactive type."))
        };
        if (!interactiveOperation.Success)
            return new(interactiveOperation);

        return new(new InteractiveByIdPublic(interactiveOperation.Value));
    }


    public async Task<OperationResult<LastInteractivePublic>> UpsertInteractiveReplyAsync(
        UpsertInteractiveReplyRequest request, User user)
    {
        var articleOperation = await _articleRepository.GetArticleByIdAsync(request.CourseId, request.ArticleId);
        if (!articleOperation.Success)
            return new(articleOperation);

        var interactiveOperation = request.Interactive switch
        {
            Interactive.CheckList => await _interactiveRepository.GetInteractiveByIdAsync<CheckListDto>(request.Interactive, request.InteractiveId),
            Interactive.ChoiceOfAnswer => await _interactiveRepository.GetInteractiveByIdAsync<ChoiceOfAnswerDto>(request.Interactive, request.InteractiveId),
            Interactive.ProgramWriting => await _interactiveRepository.GetInteractiveByIdAsync<ProgramWritingDto>(request.Interactive, request.InteractiveId),
            Interactive.WritingOfAnswer => await _interactiveRepository.GetInteractiveByIdAsync<WritingOfAnswerDto>(request.Interactive, request.InteractiveId),
            _ => new (new ArgumentException("Invalid interactive type."))
        };
        
        if (!articleOperation.Success)
            return new(articleOperation);

        dynamic lastInteractiveOperation = request.Interactive switch
        {
            Interactive.CheckList => await _interactiveRepository.UpsertInteractiveAsync(request, () =>
                    new LastCheckListDto
                    {
                        ArticleId = request.ArticleId,
                        CourseId = request.CourseId,
                        UserId = user.Id,
                        CheckedClausesJsonBoolArray = JsonConvert.SerializeObject(request.ReplyCheckList.Clauses),
                    },
                dto =>
                {
                    dto.CheckedClausesJsonBoolArray = JsonConvert.SerializeObject(request.ReplyCheckList.Clauses);
                }),
            
            
            Interactive.ChoiceOfAnswer => (await _interactiveRepository.UpsertInteractiveAsync(request, () =>
                    new LastChoiceOfAnswerDto
                    {
                        ArticleId = request.ArticleId,
                        CourseId = request.CourseId,
                        UserId = user.Id,
                        SuccessAnswersJsonBoolArray = ValidateChoiceOfAnswer(request.ReplyAnswerChoice,
                            interactiveOperation.Value.ChoiceOfAnswer),
                        AnswersJsonBoolArray = JsonConvert.SerializeObject(request.ReplyAnswerChoice.Answers),
                    },
                dto =>
                {
                    dto.SuccessAnswersJsonBoolArray = ValidateChoiceOfAnswer(request.ReplyAnswerChoice,
                        interactiveOperation.Value.ChoiceOfAnswer);
                    dto.AnswersJsonBoolArray = JsonConvert.SerializeObject(request.ReplyAnswerChoice.Answers);
                })).InvokeOnSuccess(LogChoiceOfAnswerAsync),
            
            
            Interactive.ProgramWriting => (await _interactiveRepository.UpsertInteractiveAsync(request, () =>
                    new LastProgramWritingDto
                    {
                        ArticleId = request.ArticleId,
                        CourseId = request.CourseId,
                        UserId = user.Id,
                        Code = request.ReplyProgramWriting.Code,
                        Success = ValidateProgramWriting(request.ReplyProgramWriting)
                    },
                dto =>
                {
                    dto.Code = request.ReplyProgramWriting.Code;
                    dto.Success = ValidateProgramWriting(request.ReplyProgramWriting);
                })).InvokeOnSuccess(LogProgramWritingAsync),
            
            
            Interactive.WritingOfAnswer => (await _interactiveRepository.UpsertInteractiveAsync(request, () =>
                    new LastWritingOfAnswerDto
                    {
                        ArticleId = request.ArticleId,
                        CourseId = request.CourseId,
                        UserId = user.Id,
                        Answer = request.ReplyWritingOfAnswer.Answer,
                        Success = ValidateWritingOfAnswer(request.ReplyWritingOfAnswer, interactiveOperation.Value.WritingOfAnswer)
                    },
                dto =>
                {
                    dto.Answer = request.ReplyWritingOfAnswer.Answer;
                    dto.Success = ValidateWritingOfAnswer(request.ReplyWritingOfAnswer,
                        interactiveOperation.Value.WritingOfAnswer);
                })).InvokeOnSuccess(LogWritingOfAnswerAsync),
            
            
            _ => new OperationResult<InteractivePublic>(new ArgumentException("Invalid interactive type."))
        };
        if (!lastInteractiveOperation.Success)
            return new(lastInteractiveOperation);
        
        return new(new LastInteractivePublic(lastInteractiveOperation.Value));
    }


    public async Task<OperationResult<InteractiveResponse>> GetInteractiveAsync(
        int courseId,
        int articleId,
        User user)
    {
        var articleOperation = await _articleRepository.GetArticleByIdAsync(courseId, articleId);
        if (!articleOperation.Success)
            return new(articleOperation);
        var interactiveOperation = await _interactiveRepository.GetInteractiveAsync(courseId, articleId);
        if (!interactiveOperation.Success)
            return new(interactiveOperation);
        
        var lastInteractiveOperation = await _interactiveRepository.GetLastInteractiveAsync(courseId, articleId, user);
        if (!lastInteractiveOperation.Success)
            return new(lastInteractiveOperation);
        
        return new(new InteractiveResponse(interactiveOperation.Value, lastInteractiveOperation.Value));
    }


    public async Task<OperationResult<InteractiveByIdPublic>> DeleteInteractiveAsync(Interactive interactive, int interactiveId)
    {
        return interactive switch
        {
            Interactive.CheckList => await _interactiveRepository.DeleteInteractiveByIdAsync<CheckListDto>(interactiveId),
            Interactive.ChoiceOfAnswer => await _interactiveRepository.DeleteInteractiveByIdAsync<ChoiceOfAnswerDto>(interactiveId),
            Interactive.ProgramWriting => await _interactiveRepository.DeleteInteractiveByIdAsync<ProgramWritingDto>(interactiveId),
            Interactive.WritingOfAnswer => await _interactiveRepository.DeleteInteractiveByIdAsync<WritingOfAnswerDto>(interactiveId),
            _ => new (new ArgumentException("Invalid interactive type."))
        };
    }
    
     void LogChoiceOfAnswerAsync(LastChoiceOfAnswerDto dto) =>
         _interactiveRepository.InsertInteractiveAsync(() => new LogChoiceOfAnswerDto
        {
            InteractiveId = dto.Id,
            UserId = dto.UserId,
            LogDate = DateTimeOffset.Now,
            SuccessAnswersJsonBoolArray = dto.SuccessAnswersJsonBoolArray,
            AnswersJsonBoolArray = dto.AnswersJsonBoolArray
        }).Forget();
     
     void LogProgramWritingAsync(LastProgramWritingDto dto) =>
         _interactiveRepository.InsertInteractiveAsync(() => new LogProgramWritingDto()
         {
             InteractiveId = dto.Id,
             UserId = dto.UserId,
             LogDate = DateTimeOffset.Now,
             Code = dto.Code,
             Success = dto.Success
         }).Forget();
     
     void LogWritingOfAnswerAsync(LastWritingOfAnswerDto dto) =>
         _interactiveRepository.InsertInteractiveAsync(() => new LogWritingOfAnswerDto()
         {
             InteractiveId = dto.Id,
             UserId = dto.UserId,
             LogDate = DateTimeOffset.Now,
             Success = dto.Success,
             Answer = dto.Answer
         }).Forget();
    
    private string ValidateChoiceOfAnswer(UpsertReplyAnswerChoiceRequest request, ChoiceOfAnswerDto solve) =>
        JsonConvert.SerializeObject(request.Answers.Zip(
            JsonConvert.DeserializeObject<bool[]>(solve.AnswersJsonBoolArray),
            (reply, answer) => reply && answer).ToArray());

    // TODO разработка сервиса под компиляцию и запуск кода
    private bool ValidateProgramWriting(UpsertReplyProgramWritingRequest request) =>
        request.Code.Contains("success");

    private bool ValidateWritingOfAnswer(UpsertReplyWritingOfAnswerRequest request, WritingOfAnswerDto solve) =>
        request.Answer.Equals(solve.Answer, StringComparison.CurrentCultureIgnoreCase);
}