using System.Collections.Generic;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.DataAccess.Helpers;
using HowTo.DataAccess.Repositories;
using HowTo.Entities;
using HowTo.Entities.Course;

namespace HowTo.DataAccess.Managers;

public class CourseManager
{
    private readonly CourseRepository _courseRepository;
    private readonly FileSystemHelper _fileSystemHelper;
    private readonly UserInfoManager _userInfoManager;  

    public CourseManager(CourseRepository courseRepository,
        FileSystemHelper fileSystemHelper,
        UserInfoManager userInfoManager)
    {
        _courseRepository = courseRepository;
        _fileSystemHelper = fileSystemHelper;
        _userInfoManager = userInfoManager;
    }

    public async Task<OperationResult<CoursePublic>> UpsertCourseAsync(UpsertCourseRequest request, User user)
    {
        OperationResult<CourseDto> upsertOperation;
        if (request.CourseId == null)
            upsertOperation = await _courseRepository.InsertCourseAsync(request);
        else
            upsertOperation = await _courseRepository.UpdateCourseAsync(request);

        var userOperation = await _userInfoManager.GetUserInfoAsync(user);
        if (userOperation is { Success: false, ActionStatus: ActionStatus.InternalServerError })
            return new(userOperation);
        
        if (!upsertOperation.Success)
            return new(upsertOperation);
        
        var deleteOperation = await _fileSystemHelper.DeleteCourseDirectoryAsync(upsertOperation.Value.Id);
        if (!deleteOperation.Success)
            return new(deleteOperation);
        
        if (request.File == null)
            return new(new CoursePublic(upsertOperation.Value, user, userOperation.Value));
        var saveOperation = await _fileSystemHelper.SaveCourseFilesAsync(upsertOperation.Value.Id, request.File);
        if (!saveOperation.Success)
            return new(saveOperation);
        
        return new(new CoursePublic(upsertOperation.Value, user, userOperation.Value));
    }
    public async Task<OperationResult<CoursePublic>> GetCourseWithFilesByIdAsync(int courseId, User user)
    {
        var courseOperation = await _courseRepository.GetCourseByIdAsync(courseId);
        if (!courseOperation.Success)
            return new(courseOperation);
        
        var filesOperation = await _fileSystemHelper.GetCourseFilesAsync(courseOperation.Value.Id);
        if (filesOperation.ActionStatus == ActionStatus.InternalServerError)
            return new(filesOperation);
        
        var userOperation = await _userInfoManager.GetUserInfoAsync(user);
        if (userOperation is { Success: false, ActionStatus: ActionStatus.InternalServerError })
            return new(userOperation);

        return new(new CoursePublic(
                courseOperation.Value,
                user,
                userOperation.Value,
                filesOperation.Value));
    }

    public async Task<OperationResult<CourseDto>> DeleteCourseAsync(int courseId)
    {
        var courseOperation = await _courseRepository.DeleteCourseByIdAsync(courseId);
        if (!courseOperation.Success)
            return courseOperation;
        
        var filesOperation = await _fileSystemHelper.DeleteCourseDirectoryAsync(courseOperation.Value.Id);
        if (!filesOperation.Success)
            return new(filesOperation);

        return courseOperation;
    }
    
    public Task<OperationResult<List<CourseDto>>> GetAllCoursesAsync()
    {
        return _courseRepository.GetAllCoursesAsync();
    }
}