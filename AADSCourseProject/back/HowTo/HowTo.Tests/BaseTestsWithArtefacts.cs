using ATI.Services.Common.Behaviors;
using ATI.Services.Common.Extensions.OperationResult;
using HowTo.Entities;
using HowTo.Entities.Article;
using HowTo.Entities.Course;
using HowTo.Entities.Interactive;
using HowTo.Entities.Interactive.CheckList;
using HowTo.Entities.Interactive.ChoiceOfAnswers;
using HowTo.Entities.Interactive.ProgramWriting;
using HowTo.Entities.Interactive.WritingOfAnswer;
using Microsoft.AspNetCore.Http;

namespace HowTo.Tests;

public abstract class BaseTestsWithArtefacts : BaseTests
{
    protected BaseTestsWithArtefacts(string rootPath) : base(rootPath)
    {
    }


    protected User FirstUser = new User(Guid.NewGuid(), "FirstTestUserName");
    protected User SecondUser = new User(Guid.NewGuid(), "SecondTestUserName");

    protected Task<OperationResult<CoursePublic>> InitCourseAsync(
        int? id = null,
        User? user = null,
        string? courseTitle = null,
        IFormFile? image = null) =>
        _courseManager.UpsertCourseAsync(
                new UpsertCourseRequest
                {
                    CourseId = id,
                    Title = courseTitle ?? "TestCourseTitle",
                    Description = "TestCourseDescription",
                    File = image ?? GetFormImage()
                }, user ?? FirstUser)
            .InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));


    protected Task<OperationResult<CoursePublic>> InitCourseWithFileAsync(
        int? id = null,
        User? user = null,
        string? courseTitle = null,
        IFormFile? image = null) =>
        InitCourseAsync(id, user, courseTitle, image)
            .NextAsync(coursePublic => _courseManager.GetCourseWithFilesByIdAsync(coursePublic.Id, user ?? FirstUser));


    protected Task<OperationResult<ArticlePublic>> InitArticleAsync(
        CoursePublic coursePublic,
        int? id = null,
        User? user = null,
        string? articleFileContent = null) =>
        _articleManager.UpsertArticleAsync(new UpsertArticleRequest
            {
                ArticleId = id,
                CourseId = coursePublic.Id,
                Title = "TestArticleTitle",
                File = GetFormFile(articleFileContent ?? _firstFormFileContent)
            }, user ?? FirstUser)
            .InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));


    protected Task<OperationResult<GetArticleResponse>> InitArticleWithFileAsync(
        CoursePublic coursePublic,
        int? id = null,
        User? user = null,
        string? articleFileContent = null) =>
        InitArticleAsync(coursePublic, id,  user, articleFileContent)
            .NextAsync(article =>
                _articleManager.GetArticleWithFileByIdAsync(article.CourseId, article.Id, user ?? FirstUser))
            .InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));


    protected Task<OperationResult<InteractiveByIdPublic>> InitInteractiveAsync(
        ArticlePublic articlePublic,
        int? id = null,
        UpsertCheckListRequest? checkListRequest = null,
        UpsertChoiceOfAnswerRequest? choiceOfAnswerRequest = null,
        UpsertProgramWritingRequest? programWritingRequest = null,
        UpsertWritingOfAnswerRequest? writingOfAnswerRequest = null,
        string? description = null
    ) =>
        _interactiveManager.UpsertInteractiveAsync(new UpsertInteractiveRequest
        {
            Id = id,
            ArticleId = articlePublic.Id,
            CourseId = articlePublic.CourseId,
            Description = description ?? "TestDescription",
            UpsertCheckListRequest = checkListRequest,
            UpsertChoiceOfAnswerRequest = choiceOfAnswerRequest,
            UpsertProgramWritingRequest = programWritingRequest,
            UpsertWritingOfAnswerRequest = writingOfAnswerRequest
        }).InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));
    
    protected Task<OperationResult<LastInteractivePublic>> InitInteractiveReplyAsync(
        int courseId,
        int articleId,
        int interactiveId,
        User? user = null,
        UpsertReplyCheckListRequest? checkListRequest = null,
        UpsertReplyAnswerChoiceRequest? choiceOfAnswerRequest = null,
        UpsertReplyProgramWritingRequest? programWritingRequest = null,
        UpsertReplyWritingOfAnswerRequest? writingOfAnswerRequest = null) =>
        _interactiveManager.UpsertInteractiveReplyAsync(new UpsertInteractiveReplyRequest
        {
            InteractiveId = interactiveId,
            ArticleId = articleId,
            CourseId = courseId,
            ReplyCheckList = checkListRequest,
            ReplyAnswerChoice = choiceOfAnswerRequest,
            ReplyProgramWriting = programWritingRequest,
            ReplyWritingOfAnswer = writingOfAnswerRequest
        },
            user ?? FirstUser).InvokeOnErrorAsync(operationResult => Assert.Fail(operationResult.DumpAllErrors()));
}