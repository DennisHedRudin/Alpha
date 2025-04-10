using Data.Context;
using Data.Entities;
using Data.Repositories.BaseRepository;
using Domain.Models;

namespace Data.Repositories;

public interface IClientRepository : IBaseRepository<ClientEntity, Client>
{

}

public class ClientRepository(DataContext context) : BaseRepository<ClientEntity, Client>(context), IClientRepository
{
}
