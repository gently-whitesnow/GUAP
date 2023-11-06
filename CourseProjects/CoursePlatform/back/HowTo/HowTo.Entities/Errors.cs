using System;
using ATI.Services.Common.Behaviors;

namespace HowTo.Entities;

public static class Errors
{
    public static readonly Func<int, int, OperationResult> ArticleNotFound = (courseId, articleId) =>
        new(ActionStatus.BadRequest,
            $"article with id: [{courseId}:{articleId}] not found", "article_not_found");

    public static readonly Func<int, OperationResult> CourseNotFound = (courseId) =>
        new(ActionStatus.BadRequest,
            $"course with id: [{courseId}] not found", "course_not_found");

    public static readonly Func<string, OperationResult> CourseTitleAlreadyExist = (title) =>
        new(ActionStatus.BadRequest,
            $"course with title: [{title}] already exist", "course_title_already_exist");
    
    public static readonly Func<int, int, OperationResult> ArticleFileNotFound = (courseId, articleId)  =>
        new(ActionStatus.BadRequest,
            $"article with id: [{courseId}:{articleId}] file not found", "article_file_not_found");
    
    public static readonly Func<int, OperationResult> InteractiveNotFound = (interactiveId) =>
        new(ActionStatus.BadRequest,
            $"interactive with id: [{interactiveId}] not found", "interactive_not_found");
}