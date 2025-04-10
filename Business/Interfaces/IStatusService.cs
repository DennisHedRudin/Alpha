using Business.Models;

namespace Business.Interfaces
{
    public interface IStatusService
    {
        Task<StatusResult> GetStatuses();
    }
}