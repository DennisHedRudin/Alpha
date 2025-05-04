using System.Linq.Expressions;
using Business.Interfaces;
using Business.Mappers;
using Business.Models;
using Data.Entities;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, IStatusService statusService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IStatusService _statusService = statusService;



    public async Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData)
    {
        if (formData == null)
            return new ProjectResult { Success = false, StatusCode = 400, Error = "Not all required fields are supplied." };

        var projectEntity = formData.MapTo<ProjectEntity>();
        var statusResult = await _statusService.GetStatusByIdAsync(1);
        if (!statusResult.Success)
            return new ProjectResult { Success = false, StatusCode = statusResult.StatusCode, Error = statusResult.Error };

        var status = statusResult.Result;

        projectEntity.StatusId = status!.Id;


        var result = await _projectRepository.AddAsync(projectEntity);

        return result.Success
            ? new ProjectResult { Success = true, StatusCode = 201 }
            : new ProjectResult { Success = false, StatusCode = result.StatusCode, Error = result.Error };


    }

    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync()
    {
      
        var entities = await _projectRepository.GetAllEntitiesAsync();

        var domainProjects = entities.Select(ProjectMapper.ToDomain);

        return new ProjectResult<IEnumerable<Project>>
        {
            Success = true,
            StatusCode = 200,
            Result = domainProjects
        };
    }
    

    public async Task<ProjectResult<Project>> GetProjectAsync(string id)
    {
        var response = await _projectRepository.GetAsync
           (
           where: x => x.Id == id,
           [
               include => include.Member,
               include => include.Status,
               include => include.Client
           ]
           
               );

        return response.Success
            ? new ProjectResult<Project> { Success = true, StatusCode = 200, Result = response.Result }
            : new ProjectResult<Project> { Success = false, StatusCode = 404, Error = $"Project '{id}' was not found." };
    }

    public async Task<ProjectResult> UpdateProjectAsync(EditProjectFormData formData)
    {
        if (formData == null)
            return new ProjectResult { Success = false, StatusCode = 400, Error = "Not all required fields are supplied." };

        
        var statusResult = await _statusService.GetStatusByIdAsync(formData.StatusId);
        if (!statusResult.Success)
            return new ProjectResult { Success = false, StatusCode = statusResult.StatusCode, Error = statusResult.Error };

       

        var projectEntity = formData.MapTo<ProjectEntity>();
        projectEntity.StatusId = statusResult.Result!.Id;

        var exists = await _projectRepository.ExistAsync(x => x.Id == projectEntity.Id);
        if (!exists.Success)
            return new ProjectResult { Success = false, StatusCode = 404, Error = "Project not found." };

        var result = await _projectRepository.UpdateAsync(projectEntity);
        return result.Success
            ? new ProjectResult { Success = true, StatusCode = 200 }
            : new ProjectResult { Success = false, StatusCode = result.StatusCode, Error = result.Error };
    }

    public async Task<ProjectResult> DeleteProjectAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
            return new ProjectResult { Success = false, StatusCode = 400, Error = "Project id is required." };

        var result = await _projectRepository.DeleteAsync(new ProjectEntity { Id = id });

        return result.Success
            ? new ProjectResult { Success = true, StatusCode = 200 }
            : new ProjectResult { Success = false, StatusCode = result.StatusCode, Error = result.Error };
    }


};
