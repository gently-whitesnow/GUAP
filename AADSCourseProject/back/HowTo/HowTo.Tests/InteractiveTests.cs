using ATI.Services.Common.Extensions.OperationResult;
using HowTo.Entities.Interactive;
using HowTo.Entities.Interactive.CheckList;
using HowTo.Entities.Interactive.ChoiceOfAnswers;
using HowTo.Entities.Interactive.ProgramWriting;
using HowTo.Entities.Interactive.WritingOfAnswer;
using Newtonsoft.Json;

namespace HowTo.Tests;

public class InteractiveTests : BaseTestsWithArtefacts
{
    public InteractiveTests() : base("/Users/gently/Temp/InteractiveTests-howto-test-content")
    {
    }
    
    [Fact]
    private async void CreateAllInteractiveAsync()
    {
        var courseOperation = await InitCourseAsync();
        var articleOperation = await InitArticleAsync(courseOperation.Value);
        await InitInteractiveAsync(articleOperation.Value,  checkListRequest: new UpsertCheckListRequest()
        {
            Clauses = new [] {"test1", "test2", "test3"}
        });
        await InitInteractiveAsync(articleOperation.Value,  choiceOfAnswerRequest: new UpsertChoiceOfAnswerRequest()
        {
            
            Questions = new [] {"test1", "test2", "test3"},
            Answers = new [] {true, false, false}
        });
        await InitInteractiveAsync(articleOperation.Value,  programWritingRequest:new UpsertProgramWritingRequest()
        {
            Code = "code",
            Output = "success"
        });
        await InitInteractiveAsync(articleOperation.Value,  writingOfAnswerRequest:new UpsertWritingOfAnswerRequest
        {
            Answer = "hello world"
        });

        var interactiveOperation = await 
            _interactiveManager.GetInteractiveAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
                FirstUser).InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));
        Assert.Single(interactiveOperation.Value.Interactive.CheckList);
        Assert.Single(interactiveOperation.Value.Interactive.ChoiceOfAnswer);
        Assert.Single(interactiveOperation.Value.Interactive.ProgramWriting);
        Assert.Single(interactiveOperation.Value.Interactive.WritingOfAnswer);
    }
    
    [Fact]
    private async void CreateCheckListInteractiveAsync()
    {
        var courseOperation = await InitCourseAsync();
        var articleOperation = await InitArticleAsync(courseOperation.Value);
        var firstRequest = new UpsertCheckListRequest
        {
            Clauses = new[] { "test1", "test2", "test3" }
        };
        await InitInteractiveAsync(articleOperation.Value, 
            checkListRequest: firstRequest);
        var secondRequest = new UpsertCheckListRequest
        {
            Clauses = new[] { "test3", "test2", "test1", "test0" }
        };
        await InitInteractiveAsync(articleOperation.Value, 
            checkListRequest: secondRequest);
        
        var interactiveOperation = await 
            _interactiveManager.GetInteractiveAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
                FirstUser).InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));
        Assert.True(interactiveOperation.Value.Interactive.CheckList.Length == 2);
        Assert.True(firstRequest.Clauses.SequenceEqual(interactiveOperation.Value.Interactive.CheckList[0].Clauses));
        Assert.True(secondRequest.Clauses.SequenceEqual(interactiveOperation.Value.Interactive.CheckList[1].Clauses));
    }
    
    [Fact]
    private async void CreateChoiceOfAnswerInteractiveAsync()
    {
        var courseOperation = await InitCourseAsync();
        var articleOperation = await InitArticleAsync(courseOperation.Value);
        var firstRequest = new UpsertChoiceOfAnswerRequest
        {
            Answers = new[] { true, true, false },
            Questions = new[] { "test1", "test2", "test3" }
        };
        await InitInteractiveAsync(articleOperation.Value, 
            choiceOfAnswerRequest: firstRequest);
        var secondRequest = new UpsertChoiceOfAnswerRequest
        {
            Answers = new[] { false, true },
            Questions = new[] { "test0", "test1" }
        };
        
        await InitInteractiveAsync(articleOperation.Value, 
            choiceOfAnswerRequest: secondRequest);
        
        var interactiveOperation = await 
            _interactiveManager.GetInteractiveAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
                FirstUser).InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));
        
        Assert.True(interactiveOperation.Value.Interactive.ChoiceOfAnswer.Length == 2);
        Assert.True(firstRequest.Questions.SequenceEqual(interactiveOperation.Value.Interactive.ChoiceOfAnswer[0].Questions));
        Assert.True(firstRequest.Answers.SequenceEqual(interactiveOperation.Value.Interactive.ChoiceOfAnswer[0].Answers));

        Assert.True(secondRequest.Questions.SequenceEqual(interactiveOperation.Value.Interactive.ChoiceOfAnswer[1].Questions));
        Assert.True(secondRequest.Answers.SequenceEqual(interactiveOperation.Value.Interactive.ChoiceOfAnswer[1].Answers));
    }
    
    [Fact]
    private async void CreateProgramWritingInteractiveAsync()
    {
        var courseOperation = await InitCourseAsync();
        var articleOperation = await InitArticleAsync(courseOperation.Value);
        var firstRequest = new UpsertProgramWritingRequest
        {
            Code = "code",
            Output = "output"
        };
        await InitInteractiveAsync(articleOperation.Value, 
            programWritingRequest: firstRequest);
        
        var secondRequest = new UpsertProgramWritingRequest
        {
            Code = "code2",
            Output = "output2"
        };
        await InitInteractiveAsync(articleOperation.Value, 
            programWritingRequest: secondRequest);
        
        var interactiveOperation = await 
            _interactiveManager.GetInteractiveAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
                FirstUser).InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));
        
        Assert.True(interactiveOperation.Value.Interactive.ProgramWriting.Length == 2);
        Assert.Equal(firstRequest.Code, interactiveOperation.Value.Interactive.ProgramWriting[0].Code);
        Assert.Equal(firstRequest.Output, interactiveOperation.Value.Interactive.ProgramWriting[0].Output);

        Assert.Equal(secondRequest.Code, interactiveOperation.Value.Interactive.ProgramWriting[1].Code);
        Assert.Equal(secondRequest.Output, interactiveOperation.Value.Interactive.ProgramWriting[1].Output);
    }
    
    [Fact]
    private async void CreateWritingOfAnswerInteractiveAsync()
    {
        var courseOperation = await InitCourseAsync();
        var articleOperation = await InitArticleAsync(courseOperation.Value);
        const string description = "interactiveDescription";
        var firstRequest = new UpsertWritingOfAnswerRequest
        {
            Answer = "answer",
        };
        await InitInteractiveAsync(articleOperation.Value, 
            writingOfAnswerRequest: firstRequest, description: description);
        
        var secondRequest = new UpsertWritingOfAnswerRequest
        {
            Answer = "answer2",
        };
        await InitInteractiveAsync(articleOperation.Value, 
            writingOfAnswerRequest: secondRequest, description: description);
        
        var interactiveOperation = await 
            _interactiveManager.GetInteractiveAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
                FirstUser).InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));
        
        Assert.True(interactiveOperation.Value.Interactive.WritingOfAnswer.Length == 2);

        Assert.Equal(firstRequest.Answer, interactiveOperation.Value.Interactive.WritingOfAnswer[0].Answer);
        Assert.Equal(description, interactiveOperation.Value.Interactive.WritingOfAnswer[0].Description);
        Assert.Equal(secondRequest.Answer, interactiveOperation.Value.Interactive.WritingOfAnswer[1].Answer);
        Assert.Equal(description, interactiveOperation.Value.Interactive.WritingOfAnswer[1].Description);
    }
    
    [Fact]
    private async void UpsertInteractiveAsync()
    {
        var courseOperation = await InitCourseAsync();
        var articleOperation = await InitArticleAsync(courseOperation.Value);
        var firstRequest = new UpsertCheckListRequest
        {
            Clauses = new[] { "test1", "test2", "test3" }
        };
        var upsertInteractiveOperation = await InitInteractiveAsync(articleOperation.Value, 
            checkListRequest: firstRequest);
        var secondRequest = new UpsertCheckListRequest
        {
            Clauses = new[] { "test3", "test2", "test1" }
        };
        await InitInteractiveAsync(articleOperation.Value, 
            checkListRequest: secondRequest, id:upsertInteractiveOperation.Value.CheckList.Id);
        
        var interactiveOperation = await 
            _interactiveManager.GetInteractiveAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
                FirstUser).InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));
        Assert.True(interactiveOperation.Value.Interactive.CheckList.Length == 1);
        Assert.True(secondRequest.Clauses.SequenceEqual(interactiveOperation.Value.Interactive.CheckList[0].Clauses));
    }
    
    [Fact]
    private async void CheckPrivacyInteractiveAsync()
    {
        var courseOperation = await InitCourseAsync();
        var articleOperation = await InitArticleAsync(courseOperation.Value, user: FirstUser);
        var request = new UpsertChoiceOfAnswerRequest
        {
            Answers = new[] { true, true},
            Questions = new[] { "test1", "test2" }
        };
        
        await InitInteractiveAsync(articleOperation.Value, choiceOfAnswerRequest: request);
        
        var interactiveOperationByAuthor = await 
            _interactiveManager.GetInteractiveAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
                FirstUser).InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));
        
        Assert.True(interactiveOperationByAuthor.Value.Interactive.ChoiceOfAnswer.Length == 1);
        Assert.NotNull(interactiveOperationByAuthor.Value.Interactive.ChoiceOfAnswer[0].Answers);
        Assert.True(request.Answers.SequenceEqual(interactiveOperationByAuthor.Value.Interactive.ChoiceOfAnswer[0].Answers));
        
        var interactiveOperationByAnotherUser = await 
            _interactiveManager.GetInteractiveAsync(articleOperation.Value.CourseId, articleOperation.Value.Id,
                SecondUser).InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));
        
        Assert.True(interactiveOperationByAnotherUser.Value.Interactive.ChoiceOfAnswer.Length == 1);
        Assert.Null(interactiveOperationByAnotherUser.Value.Interactive.ChoiceOfAnswer[0].Answers);
    }
}