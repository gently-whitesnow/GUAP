using ATI.Services.Common.Extensions.OperationResult;
using HowTo.Entities.Interactive;
using HowTo.Entities.Interactive.CheckList;
using HowTo.Entities.Interactive.ChoiceOfAnswers;
using HowTo.Entities.Interactive.ProgramWriting;
using HowTo.Entities.Interactive.WritingOfAnswer;

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
        await InitInteractiveAsync(articleOperation.Value, Interactive.CheckList, checkListRequest: new UpsertCheckListRequest()
        {
            Clauses = new [] {"test1", "test2", "test3"}
        });
        await InitInteractiveAsync(articleOperation.Value, Interactive.ChoiceOfAnswer, choiceOfAnswerRequest: new UpsertChoiceOfAnswerRequest()
        {
            
            Questions = new [] {"test1", "test2", "test3"},
            Answers = new [] {true, false, false}
        });
        await InitInteractiveAsync(articleOperation.Value, Interactive.ProgramWriting, programWritingRequest:new UpsertProgramWritingRequest()
        {
            Code = "code",
            Output = "success"
        });
        await InitInteractiveAsync(articleOperation.Value, Interactive.WritingOfAnswer, writingOfAnswerRequest:new UpsertWritingOfAnswerRequest
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
}