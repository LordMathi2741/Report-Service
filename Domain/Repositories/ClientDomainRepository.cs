using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Model;
using Infrastructure.Repositories;

namespace Domain.Repositories;

public class ClientDomainRepository(AppDbContext context, IUnitOfWork unitOfWork) : InfrastructureRepository<Client>(context), IClientDomainRepository
{
    public async Task<Client> AddAsync(Client client)
    {
        if (client.Number is < 100000000 or > 999999999)
        {
            throw new Exception("Client number must be 9 digits long");
        }
        await context.Set<Client>().AddAsync(client);
        await unitOfWork.SaveChangesAsync();
        return client;
    }

    public async Task<Client?> UpdateAsync(long id, Client client)
    {
        var ownerToUpdate = await context.Set<Client>().FindAsync(id);
        if (ownerToUpdate == null) throw new Exception("Client not found");
        if (client.Number is < 100000000 or > 999999999)
        {
            throw new Exception("Client number must be 9 digits long");
        }
        context.Set<Client>().Update(ownerToUpdate);
        await unitOfWork.SaveChangesAsync();
        return ownerToUpdate;
    }

    public async Task<Client?> DeleteAsync(Client client)
    {
        var ownerToDelete = await context.Set<Client>().FindAsync(client.Id);
        if (ownerToDelete == null) throw new Exception("Client not found");
        context.Set<Client>().Remove(client);
        await unitOfWork.SaveChangesAsync();
        return client;
    }
}