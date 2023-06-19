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
using HowTo.Entities.Interactive.ChoiceOfAnswer;
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

        var interactive = GetInteractive(request);
        dynamic interactiveOperation = interactive switch
        {
            Interactive.CheckList => await _interactiveRepository.UpsertInteractiveAsync(
                dto => dto.Id == request.Id,
                () => new CheckListDto
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
            
            
            Interactive.ChoiceOfAnswer => await _interactiveRepository.UpsertInteractiveAsync(
                dto => dto.Id == request.Id,
                () => new ChoiceOfAnswerDto
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
            
            
            Interactive.ProgramWriting => await _interactiveRepository.UpsertInteractiveAsync(
                dto => dto.Id == request.Id,
                () => new ProgramWritingDto
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
            
            
            Interactive.WritingOfAnswer => await _interactiveRepository.UpsertInteractiveAsync(
                dto => dto.Id == request.Id,
                () => new WritingOfAnswerDto
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

        var interactive = GetInteractiveReply(request);
        var interactiveOperation = interactive switch
        {
            Interactive.CheckList => await _interactiveRepository.GetInteractiveByIdAsync<CheckListDto>(request.InteractiveId),
            Interactive.ChoiceOfAnswer => await _interactiveRepository.GetInteractiveByIdAsync<ChoiceOfAnswerDto>(request.InteractiveId),
            Interactive.ProgramWriting => await _interactiveRepository.GetInteractiveByIdAsync<ProgramWritingDto>(request.InteractiveId),
            Interactive.WritingOfAnswer => await _interactiveRepository.GetInteractiveByIdAsync<WritingOfAnswerDto>(request.InteractiveId),
            _ => new (new ArgumentException("Invalid interactive type."))
        };
        
        if (!articleOperation.Success)
            return new(articleOperation);

        dynamic lastInteractiveOperation = interactive switch
        {
            Interactive.CheckList => await _interactiveRepository.UpsertInteractiveAsync(
                dto => dto.UserId == user.Id,
                () => new LastCheckListDto
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
            
            
            Interactive.ChoiceOfAnswer => (await _interactiveRepository.UpsertInteractiveAsync(
                dto => dto.UserId == user.Id,
                () => new LastChoiceOfAnswerDto
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
            
            
            Interactive.ProgramWriting => (await _interactiveRepository.UpsertInteractiveAsync(
                dto => dto.UserId == user.Id,
                () => new LastProgramWritingDto
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
            
            
            Interactive.WritingOfAnswer => (await _interactiveRepository.UpsertInteractiveAsync(
                dto => dto.UserId == user.Id,
                () => new LastWritingOfAnswerDto
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
        var isAuthor = articleOperation.Value.Author.UserId == user.Id;
        var interactiveOperation = await _interactiveRepository.GetInteractiveAsync(
            courseId, articleId, isAuthor);
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

    private Interactive GetInteractive(UpsertInteractiveRequest request)
    {
        if (request.UpsertCheckListRequest != null)
            return Interactive.CheckList;
        if (request.UpsertChoiceOfAnswerRequest != null)
            return Interactive.ChoiceOfAnswer;
        if (request.UpsertProgramWritingRequest != null)
            return Interactive.ProgramWriting;
        if (request.UpsertWritingOfAnswerRequest != null)
            return Interactive.WritingOfAnswer;
        
        throw new NotImplementedException("Interactive body not implemented");
    }
    
    private Interactive GetInteractiveReply(UpsertInteractiveReplyRequest request)
    {
        if (request.ReplyCheckList != null)
            return Interactive.CheckList;
        if (request.ReplyAnswerChoice != null)
            return Interactive.ChoiceOfAnswer;
        if (request.ReplyProgramWriting != null)
            return Interactive.ProgramWriting;
        if (request.ReplyWritingOfAnswer != null)
            return Interactive.WritingOfAnswer;
        
        throw new NotImplementedException("Interactive body not implemented");
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
    
    private string ValidateChoiceOfAnswer(UpsertReplyAnswerChoiceRequest request, ChoiceOfAnswerPublic solve) =>
        JsonConvert.SerializeObject(request.Answers.Zip(solve.Answers,
            (reply, answer) => reply == answer).ToArray());

    // TODO разработка сервиса под компиляцию и запуск кода
    private bool ValidateProgramWriting(UpsertReplyProgramWritingRequest request) =>
        request.Code.Contains("success");

    private bool ValidateWritingOfAnswer(UpsertReplyWritingOfAnswerRequest request, WritingOfAnswerPublic solve) =>
        request.Answer.Equals(solve.Answer, StringComparison.CurrentCultureIgnoreCase);
}