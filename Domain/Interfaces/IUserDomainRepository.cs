using Infrastructure.Interfaces;
using Infrastructure.Model;

namespace Domain.Interfaces;

public interface IUserDomainRepository : IInfrastructureRepository<User>
{
    Task<User> AddAsync(User user);
    Task<User?> UpdateAsync(User user);
    Task<User?> DeleteAsync(User user);
    Task<string?> GetUserRoleByUsername(string username);
    Task<User?> GetUserByEmailAndPassword(string email, string password);
}