using Data.Context;
using Data.Entities;
using Data.Repositories.BaseRepository;
using Domain.Models;

namespace Data.Repositories;

public interface IProjectRepository : IBaseRepository<ProjectEntity, Project>
{

}

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity, Project>(context), IProjectRepository
{
}
