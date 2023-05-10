using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.Entities;
using HowTo.Entities.Article;
using HowTo.Entities.Course;
using Microsoft.EntityFrameworkCore;

namespace HowTo.DataAccess.Repositories;

public class CourseRepository
{
    private readonly ApplicationContext _db;

    public CourseRepository(ApplicationContext applicationContext)
    {
        _db = applicationContext;
    }

    public async Task<OperationResult<CourseDto>> InsertCourseAsync(UpsertCourseRequest request, User user)
    {
        var dto = new CourseDto
        {
            Title = request.Title,
            Description = request.Description,
            Path = request.Path,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Contributors = new List<User> { user },
        };
        try
        {
            await _db.CourseDtos.AddAsync(dto);
            await _db.SaveChangesAsync();
            return new(dto);
        }
        catch (Exception ex)
        {
            return new OperationResult<CourseDto>(ex);
        }
    }

    public async Task<OperationResult<CourseDto>> UpdateCourseAsync(UpsertCourseRequest request, User user)
    {
        try
        {
            var courseDto = await _db.CourseDtos.FirstOrDefaultAsync(c => c.Id == request.CourseId);
            if (courseDto == null)
            {
                return new(ActionStatus.NotFound, "course_not_found", $"Course with id {request.CourseId} not found");
            }

            courseDto.UpdatedAt = DateTime.UtcNow;
            courseDto.Title = request.Title;
            courseDto.Description = request.Description;
            courseDto.Path = request.Path;
            if (courseDto.Contributors.All(u => u.Id != user.Id))
                courseDto.Contributors.Add(user);

            await _db.SaveChangesAsync();
            return new(courseDto);
        }
        catch (Exception ex)
        {
            return new OperationResult<CourseDto>(ex);
        }
    }

    public async Task<OperationResult<CourseDto>> UpsertArticleToCourseAsync(int courseId, ArticleDto article, User user)
    {
        try
        {
            var courseDto = await _db.CourseDtos.FirstOrDefaultAsync(c => c.Id == courseId);
            if (courseDto == null)
            {
                return new(ActionStatus.NotFound, "course_not_found", $"Course with id {courseId} not found");
            }

            if (courseDto.Articles.Contains(article))
                return new(courseDto);
            
            courseDto.Articles.Add(article);
            courseDto.UpdatedAt = DateTimeOffset.Now;
            if (courseDto.Contributors.All(u => u.Id != user.Id))
                courseDto.Contributors.Add(user);

            await _db.SaveChangesAsync();
            return new(courseDto);
        }
        catch (Exception ex)
        {
            return new OperationResult<CourseDto>(ex);
        }
    }


    public async Task<OperationResult<CourseDto>> GetCourseByPathAsync(string coursePath)
    {
        try
        {
            var courseDto = await _db.CourseDtos.FirstOrDefaultAsync(c => c.Path == coursePath);
            if (courseDto == null)
            {
                return new(ActionStatus.NotFound, "course_not_found", $"Course with path {coursePath} not found");
            }

            return new(courseDto);
        }
        catch (Exception ex)
        {
            return new OperationResult<CourseDto>(ex);
        }
    }

    public async Task<OperationResult<CourseDto>> GetCourseByIdAsync(int courseId)
    {
        try
        {
            var courseDto = await _db.CourseDtos.FirstOrDefaultAsync(c => c.Id == courseId);
            if (courseDto == null)
            {
                return new(ActionStatus.NotFound, "course_not_found", $"Course with id {courseId} not found");
            }

            return new(courseDto);
        }
        catch (Exception ex)
        {
            return new OperationResult<CourseDto>(ex);
        }
    }

    public async Task<OperationResult<CourseDto>> DeleteCourseByIdAsync(int courseId)
    {
        try
        {
            var courseDto = await _db.CourseDtos.FirstOrDefaultAsync(c => c.Id == courseId);
            if (courseDto == null)
            {
                return new(ActionStatus.NotFound, "course_not_found", $"Course with id {courseId} not found");
            }

            _db.CourseDtos.Remove(courseDto);
            await _db.SaveChangesAsync();
            return new(courseDto);
        }
        catch (Exception ex)
        {
            return new OperationResult<CourseDto>(ex);
        }
    }

    public async Task<OperationResult<List<CourseDto>>> GetAllCoursesAsync()
    {
        try
        {
            return new(await _db.CourseDtos.ToListAsync());
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}