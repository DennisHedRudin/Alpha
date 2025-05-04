using System.Linq.Expressions;
using System.Linq;
using Data.Context;
using Data.Entities;
using Data.Models;
using Data.Repositories.BaseRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IProjectRepository : IBaseRepository<ProjectEntity, Project>
{    
    Task<IEnumerable<ProjectEntity>> GetAllEntitiesAsync();
}

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity, Project>(context), IProjectRepository
{
    public async Task<IEnumerable<ProjectEntity>> GetAllEntitiesAsync()
    {
        var query = _table
            .Include(p => p.Client)
            .Include(p => p.Member)
            .Include(p => p.Status);

        return await query.ToListAsync();
    }

    public override async Task<RepositoryResult<bool>> DeleteAsync(ProjectEntity entity)
    {
        try
        {
            var trackedEntity = await _table.FindAsync(entity.Id);
            if (trackedEntity == null)
                return new RepositoryResult<bool> { Success = false, StatusCode = 404, Error = "Project not found." };

            _table.Remove(trackedEntity);
            await _context.SaveChangesAsync();

            return new RepositoryResult<bool> { Success = true, StatusCode = 200 };
        }
        catch (Exception)
        {
            return new RepositoryResult<bool> { Success = false, StatusCode = 500, Error = "Unable to delete project." };
        }
    }


}
