using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.Entities;
using HowTo.Entities.Contributor;
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

    public async Task<OperationResult<CourseDto>> InsertCourseAsync(UpsertCourseRequest request)
    {
        try
        {
            var courseDto = await _db.CourseContext.SingleOrDefaultAsync(c => c.Title == request.Title);
            if (courseDto != null)
                return new (Errors.CourseTitleAlreadyExist(courseDto.Title));

            var dto = new CourseDto
            {
                Title = request.Title,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _db.CourseContext.AddAsync(dto);
            await _db.SaveChangesAsync();
            return new (dto);
        }
        catch (Exception ex)
        {
            return new (ex);
        }
    }

    public async Task<OperationResult<CourseDto>> UpdateCourseAsync(UpsertCourseRequest request)
    {
        try
        {
            var courseDto = await _db.CourseContext.FirstOrDefaultAsync(c => c.Id == request.CourseId);
            if (courseDto == null)
                return new(Errors.CourseNotFound(request.CourseId!.Value));

            courseDto.UpdatedAt = DateTime.UtcNow;
            courseDto.Title = request.Title;
            courseDto.Description = request.Description;

            await _db.SaveChangesAsync();
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
            var courseDto = await _db.CourseContext
                .Include(c => c.Articles)
                .ThenInclude(a=>a.Author)
                .SingleOrDefaultAsync(c => c.Id == courseId);
            if (courseDto == null)
                return new(Errors.CourseNotFound(courseId));

            return new(courseDto);
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }

    public async Task<OperationResult<CourseDto>> DeleteCourseByIdAsync(int courseId)
    {
        try
        {
            var courseDto = await _db.CourseContext
                .Include(d=>d.Articles)
                .ThenInclude(a=>a.Author)
                .SingleOrDefaultAsync(c => c.Id == courseId);
            if (courseDto == null)
                return new(Errors.CourseNotFound(courseId));

            _db.CourseContext.Remove(courseDto);
            foreach (var articleDto in courseDto.Articles)
            {
                _db.ArticleContext.Remove(articleDto);
            }
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
            return new(await _db.CourseContext
                .Include(d=>d.Articles)
                .ToListAsync());
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}