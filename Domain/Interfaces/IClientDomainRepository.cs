using Infrastructure.Interfaces;
using Infrastructure.Model;

namespace Domain.Interfaces;

public interface IClientDomainRepository : IInfrastructureRepository<Client>
{
    Task<Client> AddAsync(Client client);
    Task<Client?> UpdateAsync(long id,Client client);
    Task<Client?> DeleteAsync(Client client);
}