using Business.Interfaces;
using Business.Models;
using Data.Repositories;
using Domain.Extensions;

namespace Business.Services;


public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<StatusResult> GetStatuses()
    {
        var result = await _statusRepository.GetAll();
        return result.MapTo<StatusResult>();
    }
}
