using System;
using System.Linq;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.Entities;
using HowTo.Entities.Interactive;
using HowTo.Entities.Interactive.Base;
using Microsoft.EntityFrameworkCore;

namespace HowTo.DataAccess.Repositories;

public class InteractiveRepository
{
    private readonly ApplicationContext _db;

    public InteractiveRepository(ApplicationContext applicationContext)
    {
        _db = applicationContext;
    }
    
    public async Task<OperationResult<TDto>> UpsertInteractiveAsync<TDto>(
        IHaveNullableId request,
        Func<TDto> getFunc,
        Action<TDto> updateFunc
    )  where TDto : class, IHaveId, new() 
    {
        try
        {
            var dbContext = _db.Set<TDto>();
            var interactiveDto =  await dbContext.SingleOrDefaultAsync(c => c.Id == request.Id);
            if (interactiveDto == null)
            {
                return await InsertInteractiveAsync(getFunc);
            }
            updateFunc(interactiveDto);
            await _db.SaveChangesAsync();
            return new(interactiveDto);
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
    
    public async Task<OperationResult<TDto>> InsertInteractiveAsync<TDto>(
        Func<TDto> getFunc
    )  where TDto : class, IHaveId, new() 
    {
        try
        {
            var dto = getFunc();
            await _db.Set<TDto>().AddAsync(dto);
            await _db.SaveChangesAsync();
            return new(dto);
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }

    public async Task<OperationResult<InteractivePublic>> GetInteractiveAsync(int courseId, int articleId)
    {
        try
        {
            var checkListDto = await _db.CheckListContext.AsQueryable()
                .Where(a => a.CourseId == courseId && a.ArticleId == articleId).ToArrayAsync();
            var choiceOfAnswerDto = await _db.ChoiceOfAnswerContext.AsQueryable()
                .Where(a => a.CourseId == courseId && a.ArticleId == articleId).ToArrayAsync();
            var programWritingDto = await _db.ProgramWritingContext.AsQueryable()
                .Where(a => a.CourseId == courseId && a.ArticleId == articleId).ToArrayAsync();
            var writingOfAnswerDto = await _db.WritingOfAnswerContext.AsQueryable()
                .Where(a => a.CourseId == courseId && a.ArticleId == articleId).ToArrayAsync();

            return new(new InteractivePublic(checkListDto, choiceOfAnswerDto, programWritingDto, writingOfAnswerDto));
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
    
    public async Task<OperationResult<LastInteractivePublic>> GetLastInteractiveAsync(int courseId, int articleId, User user)
    {
        try
        {
            var checkListDto = await _db.LastCheckListContext.SingleOrDefaultAsync
                (a => a.CourseId == courseId && a.ArticleId == articleId && a.UserId == user.Id);
            var choiceOfAnswerDto = await _db.LastChoiceOfAnswerContext.SingleOrDefaultAsync
                (a => a.CourseId == courseId && a.ArticleId == articleId && a.UserId == user.Id);
            var programWritingDto = await _db.LastProgramWritingContext.SingleOrDefaultAsync
                (a => a.CourseId == courseId && a.ArticleId == articleId && a.UserId == user.Id);
            var writingOfAnswerDto = await _db.LastWritingOfAnswerContext.SingleOrDefaultAsync
                (a => a.CourseId == courseId && a.ArticleId == articleId && a.UserId == user.Id);

            return new(new LastInteractivePublic(checkListDto, choiceOfAnswerDto, programWritingDto, writingOfAnswerDto));
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }

    public async Task<OperationResult<InteractiveByIdPublic>> GetInteractiveByIdAsync<TDto>(Interactive interactive,
        int interactiveId) where TDto: class, IHaveId
    {
        try
        {
            var dbContext = _db.Set<TDto>();
            dynamic interactiveDto = await dbContext.SingleOrDefaultAsync(c => c.Id == interactiveId);

            return interactiveDto != null
                ? new (new InteractiveByIdPublic(interactiveDto))
                : new (Errors.InteractiveNotFound(interactiveId));
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }

    public async Task<OperationResult<InteractiveByIdPublic>> DeleteInteractiveByIdAsync<TDto>(int interactiveId)
        where TDto: class, IHaveId
    {
        try
        {
            var dbContext = _db.Set<TDto>();
            dynamic interactiveDto = await dbContext.SingleOrDefaultAsync(c => c.Id == interactiveId);

            if (interactiveDto == null)
                return new (Errors.InteractiveNotFound(interactiveId));

            dbContext.Remove(interactiveDto);
            await _db.SaveChangesAsync();
            return new (new InteractiveByIdPublic(interactiveDto));
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}