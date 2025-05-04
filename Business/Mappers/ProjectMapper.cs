using Data.Entities;
using Domain.Models;

namespace Business.Mappers;

public class ProjectMapper
{

    public static Project ToDomain(ProjectEntity entity)
    {
        return new Project
        {
            Id = entity.Id,
            Image = entity.Image,
            ProjectName = entity.ProjectName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Budget = entity.Budget,

            Client = new Client
            {
                Id = entity.Client.Id,
                ClientName = entity.Client.ClientName,                
            },

            Member = new Member
            {
                Id = entity.Member.Id,
                FirstName = entity.Member.FirstName,
                LastName = entity.Member.LastName,
                Email = entity.Member.Email,
                PhoneNumber = entity.Member.PhoneNumber,
                JobTitle = entity.Member.JobTitle                
            },

            Status = new Status
            {
                Id = entity.Status.Id,
                StatusName = entity.Status.StatusName
            }
        };
    }
}
