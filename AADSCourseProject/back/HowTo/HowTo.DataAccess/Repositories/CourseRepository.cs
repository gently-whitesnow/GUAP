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

    public async Task<OperationResult<CourseDto>> InsertCourseAsync(UpsertCourseRequest request, User user)
    {
        try
        {
            var courseDto = await _db.CourseDtos.SingleOrDefaultAsync(c => c.Title == request.Title);
            if (courseDto != null)
                return new (Errors.CourseTitleAlreadyExist(courseDto.Title));

            var dto = new CourseDto
            {
                Title = request.Title,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Contributors = new List<ContributorEntity>
                {
                    new()
                    {
                        UserId = user.Id,
                        Name = user.Name
                    }
                }
            };

            await _db.CourseDtos.AddAsync(dto);
            await _db.SaveChangesAsync();
            return new (dto);
        }
        catch (Exception ex)
        {
            return new (ex);
        }
    }

    public async Task<OperationResult<CourseDto>> UpdateCourseAsync(UpsertCourseRequest request, User user)
    {
        try
        {
            var courseDto = await _db.CourseDtos.Include(d=>d.Contributors)
                .FirstOrDefaultAsync(c => c.Id == request.CourseId);
            if (courseDto == null)
                return new(Errors.CourseNotFound(request.CourseId!.Value));

            courseDto.UpdatedAt = DateTime.UtcNow;
            courseDto.Title = request.Title;
            courseDto.Description = request.Description;
            if (courseDto.Contributors.All(u => u.UserId != user.Id))
                courseDto.Contributors.Add(new ContributorEntity
                {
                    UserId = user.Id,
                    Name = user.Name
                });

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
            var courseDto = await _db.CourseDtos
                .Include(c => c.Contributors)
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
            var courseDto = await _db.CourseDtos
                .Include(d=>d.Contributors)
                .Include(d=>d.Articles)
                .ThenInclude(a=>a.Author)
                .SingleOrDefaultAsync(c => c.Id == courseId);
            if (courseDto == null)
                return new(Errors.CourseNotFound(courseId));

            _db.CourseDtos.Remove(courseDto);
            foreach (var articleDto in courseDto.Articles)
            {
                _db.ArticleDtos.Remove(articleDto);
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
            return new(await _db.CourseDtos
                .Include(d=>d.Articles)
                .Include(d=>d.Contributors)
                .ToListAsync());
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}