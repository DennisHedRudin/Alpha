using Business.Models;
using Domain.Models;

namespace Business.Services
{
    public interface IStatusService
    {
        Task<StatusResult<Status>> GetStatusByIdAsync(int id);
        Task<StatusResult<Status>> GetStatusByNameAsync(string statusName);
        Task<StatusResult<IEnumerable<Status>>> GetStatusesAsync();
    }
}