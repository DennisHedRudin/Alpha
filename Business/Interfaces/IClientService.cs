using Business.Models;
using Domain.Models;

namespace Business.Interfaces
{
    public interface IClientService
    {
        Task<ClientResult> GetClientsAsync();
        Task<ClientResult> AddClientAsync(Client form)
    }
}