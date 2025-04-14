using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;

namespace Business.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<ClientResult> GetClientsAsync()
    {
        var result = await _clientRepository.GetAllAsync();
        return result.MapTo<ClientResult>();
    }

    public async Task<ClientResult> AddClientAsync(Client form)
    {
        if (form == null)
            return new ClientResult { Success = false, StatusCode = 400, Error = "Form data can't be null." };

        var existsResult = await _clientRepository.ExistAsync(x => x.ClientName == form.ClientName);
        if (existsResult.Success)
            return new ClientResult { Success = false, StatusCode = 409, Error = "Client already exists." };

        try
        {
            var entity = form.MapTo<ClientEntity>();
            entity.Id = Guid.NewGuid().ToString();

            var result = await _clientRepository.AddAsync(entity);
            return new ClientResult { Success = result.Success,StatusCode = result.StatusCode,Error = result.Error};
        }
        catch (Exception ex)
        {
            return new ClientResult { Success = false, StatusCode = 500, Error = ex.Message };
        }
    }
}
