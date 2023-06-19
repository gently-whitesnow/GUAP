using ATI.Services.Common.Extensions.OperationResult;
using HowTo.Entities.Interactive;
using HowTo.Entities.Interactive.CheckList;
using HowTo.Entities.Interactive.ChoiceOfAnswers;
using HowTo.Entities.Interactive.ProgramWriting;
using HowTo.Entities.Interactive.WritingOfAnswer;
using Newtonsoft.Json;

namespace HowTo.Tests;

public class InteractiveReplyTests : BaseTestsWithArtefacts
{
    public InteractiveReplyTests() : base("/Users/gently/Temp/InteractiveReplyTests-howto-test-content")
    {
    }
    
    [Fact]
    private async void CreateAllInteractiveAndReplyAsync()
    {
        var courseOperation = await InitCourseAsync();
        var articleOperation = await InitArticleAsync(courseOperation.Value);
        var checkListOperation = await InitInteractiveAsync(articleOperation.Value,  checkListRequest: new UpsertCheckListRequest()
        {
            Clauses = new [] {"test1", "test2", "test3"}
        });
        var choiceOfAnswerOperation = await InitInteractiveAsync(articleOperation.Value,  choiceOfAnswerRequest: new UpsertChoiceOfAnswerRequest()
        {
            
            Questions = new [] {"test1", "test2", "test3"},
            Answers = new [] {true, false, false}
        });
        var programWritingOperation = await InitInteractiveAsync(articleOperation.Value, programWritingRequest:new UpsertProgramWritingRequest()
        {
            Code = "code",
            Output = "success"
        });
        var writingOfAnswerOperation = await InitInteractiveAsync(articleOperation.Value, writingOfAnswerRequest:new UpsertWritingOfAnswerRequest
        {
            Answer = "hello world"
        });
        
        await InitInteractiveReplyAsync(articleOperation.Value.CourseId,articleOperation.Value.Id,checkListOperation.Value.CheckList.Id,
            checkListRequest: new UpsertReplyCheckListRequest
        {
            Clauses = new [] {true,false, true}
        });
        await InitInteractiveReplyAsync(articleOperation.Value.CourseId,articleOperation.Value.Id,choiceOfAnswerOperation.Value.ChoiceOfAnswer.Id,
            choiceOfAnswerRequest: new UpsertReplyAnswerChoiceRequest
        {
            Answers = new [] {false, false, true}
        });
        await InitInteractiveReplyAsync(articleOperation.Value.CourseId,articleOperation.Value.Id,programWritingOperation.Value.ProgramWriting.Id,
            programWritingRequest: new UpsertReplyProgramWritingRequest()
        {
            Code = "codeuser",
        });
        await InitInteractiveReplyAsync(articleOperation.Value.CourseId,articleOperation.Value.Id,writingOfAnswerOperation.Value.WritingOfAnswer.Id,
            writingOfAnswerRequest: new UpsertReplyWritingOfAnswerRequest
        {
            Answer = "answer from user"
        });

        var interactiveOperation = await 
            _interactiveManager.GetInteractiveAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
                FirstUser).InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));
        Assert.NotNull(interactiveOperation.Value.LastInteractive.LastCheckList);
        Assert.NotNull(interactiveOperation.Value.LastInteractive.LastChoiceOfAnswer);
        Assert.NotNull(interactiveOperation.Value.LastInteractive.LastProgramWriting);
        Assert.NotNull(interactiveOperation.Value.LastInteractive.LastWritingOfAnswer);
    }
    
    [Fact]
    private async void CreateCheckListInteractiveAndReplyAsync()
    {
        var courseOperation = await InitCourseAsync();
        var articleOperation = await InitArticleAsync(courseOperation.Value);
        var request = new UpsertCheckListRequest
        {
            Clauses = new[] { "test1", "test2", "test3" }
        };
        var initInteractiveOperation = await InitInteractiveAsync(articleOperation.Value, 
            checkListRequest: request);
        await InitInteractiveReplyAsync(articleOperation.Value.CourseId,articleOperation.Value.Id,initInteractiveOperation.Value.CheckList.Id,
            checkListRequest: new UpsertReplyCheckListRequest
            {
                Clauses = new [] {true,false, true}
            });
        
        var lastReply = new UpsertReplyCheckListRequest
        {
            Clauses = new [] {true ,false, true}
        };
        await InitInteractiveReplyAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
            initInteractiveOperation.Value.CheckList.Id,
            checkListRequest: lastReply);
        
        var interactiveOperation = await 
            _interactiveManager.GetInteractiveAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
                FirstUser).InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));
        Assert.NotNull(interactiveOperation.Value.LastInteractive.LastCheckList);
        Assert.True(lastReply.Clauses.SequenceEqual(interactiveOperation.Value.LastInteractive.LastCheckList.Clauses));
    }
    
    [Fact]
    private async void CreateChoiceOfAnswerInteractiveAndReplyAsync()
    {
        var courseOperation = await InitCourseAsync();
        var articleOperation = await InitArticleAsync(courseOperation.Value);
        var request = new UpsertChoiceOfAnswerRequest
        {
            Answers = new[] { true, true, false },
            Questions = new[] { "test1", "test2", "test3" }
        };

        var initInteractiveOperation = await InitInteractiveAsync(articleOperation.Value, 
            choiceOfAnswerRequest: request);
        
        await InitInteractiveReplyAsync(articleOperation.Value.CourseId,articleOperation.Value.Id,initInteractiveOperation.Value.ChoiceOfAnswer.Id,
            choiceOfAnswerRequest: new UpsertReplyAnswerChoiceRequest
            {
                Answers = new [] {false ,false, false}
            });
        
        var lastReply = new UpsertReplyAnswerChoiceRequest
        {
            Answers = new [] {true ,true, false}
        };
        var successReply = new UpsertReplyAnswerChoiceRequest
        {
            Answers = new [] {true ,true, true}
        };
        await InitInteractiveReplyAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
            initInteractiveOperation.Value.ChoiceOfAnswer.Id,
            choiceOfAnswerRequest: lastReply);
        
        var interactiveOperation = await 
            _interactiveManager.GetInteractiveAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
                FirstUser).InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));
        Assert.NotNull(interactiveOperation.Value.LastInteractive.LastChoiceOfAnswer);
        Assert.True(lastReply.Answers.SequenceEqual(interactiveOperation.Value.LastInteractive.LastChoiceOfAnswer.Answers));
        Assert.True(successReply.Answers.SequenceEqual(interactiveOperation.Value.LastInteractive.LastChoiceOfAnswer.SuccessAnswers));
    }
    
    [Fact]
    private async void CreateProgramWritingInteractiveReplyAsync()
    {
        var courseOperation = await InitCourseAsync();
        var articleOperation = await InitArticleAsync(courseOperation.Value);
        var request = new UpsertProgramWritingRequest
        {
            Code = "code",
            Output = "output"
        };
        var initInteractiveOperation = await InitInteractiveAsync(articleOperation.Value, 
            programWritingRequest: request);
        
        await InitInteractiveReplyAsync(articleOperation.Value.CourseId,articleOperation.Value.Id,initInteractiveOperation.Value.ProgramWriting.Id,
            programWritingRequest: new UpsertReplyProgramWritingRequest
            {
                Code = "false"
            });
        
        var lastReply = new UpsertReplyProgramWritingRequest
        {
            Code = "success"
        };
        
        await InitInteractiveReplyAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
            initInteractiveOperation.Value.ProgramWriting.Id,
            programWritingRequest: lastReply);
        
        var interactiveOperation = await 
            _interactiveManager.GetInteractiveAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
                FirstUser).InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));
        Assert.NotNull(interactiveOperation.Value.LastInteractive.LastProgramWriting);
        Assert.Equal(lastReply.Code, interactiveOperation.Value.LastInteractive.LastProgramWriting.Code);
        Assert.True(interactiveOperation.Value.LastInteractive.LastProgramWriting.Success);
    }
    
    [Fact]
    private async void CreateWritingOfAnswerInteractiveReplyAsync()
    {
        var courseOperation = await InitCourseAsync();
        var articleOperation = await InitArticleAsync(courseOperation.Value);
        const string description = "interactiveDescription";
        var request = new UpsertWritingOfAnswerRequest
        {
            Answer = "success",
        };
        var initInteractiveOperation = await InitInteractiveAsync(articleOperation.Value, 
            writingOfAnswerRequest: request, description: description);
        
        await InitInteractiveReplyAsync(articleOperation.Value.CourseId,articleOperation.Value.Id,initInteractiveOperation.Value.WritingOfAnswer.Id,
            writingOfAnswerRequest: new UpsertReplyWritingOfAnswerRequest()
            {
                Answer = "bad"
            });
        
        var lastReply = new UpsertReplyWritingOfAnswerRequest()
        {
            Answer = "success"
        };
        
        await InitInteractiveReplyAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
            initInteractiveOperation.Value.WritingOfAnswer.Id,
            writingOfAnswerRequest: lastReply);
        
        var interactiveOperation = await 
            _interactiveManager.GetInteractiveAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
                FirstUser).InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));
        Assert.NotNull(interactiveOperation.Value.LastInteractive.LastWritingOfAnswer);
        Assert.Equal(lastReply.Answer, interactiveOperation.Value.LastInteractive.LastWritingOfAnswer.Answer);
        Assert.True(interactiveOperation.Value.LastInteractive.LastWritingOfAnswer.Success);
    }
}