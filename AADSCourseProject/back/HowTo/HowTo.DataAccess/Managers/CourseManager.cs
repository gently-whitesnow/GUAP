using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.DataAccess.Helpers;
using HowTo.DataAccess.Repositories;
using HowTo.Entities;
using HowTo.Entities.Article;
using HowTo.Entities.Course;

namespace HowTo.DataAccess.Managers;

public class CourseManager
{
    private readonly CourseRepository _courseRepository;
    private readonly FileSystemHelper _fileSystemHelper;

    public CourseManager(CourseRepository courseRepository,
        FileSystemHelper fileSystemHelper)
    {
        _courseRepository = courseRepository;
        _fileSystemHelper = fileSystemHelper;
    }

    public async Task<OperationResult<CourseDto>> UpsertCourseAsync(UpsertCourseRequest request, User user)
    {
        OperationResult<CourseDto> upsertOperation;
        if (request.CourseId == null)
            upsertOperation = await _courseRepository.InsertCourseAsync(request, user);
        else
            upsertOperation = await _courseRepository.UpdateCourseAsync(request, user);

        if (!upsertOperation.Success || request.Image == null)
            return upsertOperation;

        var deleteOperation = await _fileSystemHelper.DeleteCourseFilesAsync(request.CourseId.Value);
        if (!deleteOperation.Success)
            return new(deleteOperation);
        
        var saveOperation = await _fileSystemHelper.SaveCourseFilesAsync(request.CourseId.Value, request.Image);
        if (!saveOperation.Success)
            return new(saveOperation);
        return upsertOperation;
    }

    public Task<OperationResult<CourseDto>> UpsertArticleToCourseAsync(int courseId, ArticleDto article, User user)
    {
        return _courseRepository.UpsertArticleToCourseAsync(courseId, article, user);
    }

    public async Task<OperationResult<GetCourseResponse>> GetCourseByPathAsync(string coursePath)
    {
        var courseOperation = await _courseRepository.GetCourseByPathAsync(coursePath);
        if (!courseOperation.Success)
            return new(courseOperation);
        var filesOperation = await _fileSystemHelper.GetCourseFilesAsync(courseOperation.Value.Id);
        if (!filesOperation.Success)
            return new(filesOperation);

        return new(new GetCourseResponse(courseOperation.Value, filesOperation.Value));
    }

    public Task<OperationResult<CourseDto>> GetCourseByIdAsync(int courseId)
    {
        return _courseRepository.GetCourseByIdAsync(courseId);
    }

    public async Task<OperationResult<CourseDto>> DeleteCourseAsync(int courseId)
    {
        var courseOperation = await _courseRepository.DeleteCourseByIdAsync(courseId);
        if (!courseOperation.Success)
            return courseOperation;
        var filesOperation = await _fileSystemHelper.DeleteCourseFilesAsync(courseOperation.Value.Id);
        if (!filesOperation.Success)
            return new(filesOperation);

        return courseOperation;
    }
    
    public Task<OperationResult<List<CourseDto>>> GetAllCoursesAsync()
    {
        return _courseRepository.GetAllCoursesAsync();
    }
}