using Infrastructure.Interfaces;
using Infrastructure.Model;

namespace Domain.Interfaces;

public interface IUserDomainRepository : IInfrastructureRepository<User>
{
    Task<User> AddAsync(User user);
    Task<User?> UpdateAsync(long id,User user);
    Task<User?> DeleteAsync(User user);
}