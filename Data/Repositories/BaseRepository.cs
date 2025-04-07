using System.Diagnostics;
using System.Linq.Expressions;
using Data.Context;
using Data.Extentions;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context;
    protected readonly DbSet<TEntity> _table;

    protected BaseRepository(DataContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public virtual async Task<RepositoryResult<bool>> AddAsync(TEntity entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { Success = false, StatusCode = 400, Error = "Entity can't be null."};

        try
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Success = true, StatusCode = 201 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult<bool> { Success = false, StatusCode = 500, Error = ex.Message };
        }
    }

    public virtual async Task<RepositoryResult<IEnumerable<T>>> GetAllAsync()
    {
        var entities = await _table.ToListAsync();
        var result = entities.Select(entity => entity.MapTo<T>);
        return new RepositoryResult<IEnumerable<T>> { Success = true, StatusCode = 200, Result = result };
    }

    public virtual async Task<RepositoryResult<T>> GetAsync<T>(Expression<Func<TEntity, bool>> findBy)
    {
        var entity = await _table.FirstOrDefaultAsync(findBy);
        if(entity == null)
            return new RepositoryResult<T> { Success = false, StatusCode = 404, Error = "Entity not found." }

        var result = entity            
            return new RepositoryResult<T> { Success = true, StatusCode = 200, Result = result };
    }


    public virtual async Task<RepositoryResult<bool>> ExistAsync(Expression<Func<TEntity, bool>> findBy)
    {
        var exist = await _table.AnyAsync(findBy);
        return !exist
            ? new RepositoryResult<bool> { Success = false, StatusCode = 404, Error = "Entity not found." }
            : new RepositoryResult<bool> { Success = true, StatusCode = 200 };
    }


    public virtual async Task<RepositoryResult<bool>> UpdateAsync(TEntity entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { Success = false, StatusCode = 400, Error = "Entity can't be null." };

        try
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Success = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult<bool> { Success = false, StatusCode = 500, Error = ex.Message };
        }
    }

    public virtual async Task<RepositoryResult<bool>> DeleteAsync(TEntity entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { Success = false, StatusCode = 400, Error = "Entity can't be null." };

        try
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Success = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult<bool> { Success = false, StatusCode = 500, Error = ex.Message };
        }
    }
}
