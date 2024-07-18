using Infrastructure.Interfaces;
using Infrastructure.Model;

namespace Domain.Interfaces;

public interface IUserDomainRepository : IInfrastructureRepository<User>
{
    Task<User> SignUp(User user);
    Task<User?> UpdateAsync(User user);
    Task<User?> DeleteAsync(User user);
    Task<User?> GetUsernameByEmail(string email);
    Task<string?> GetUserRoleByUsername(string username);
    Task<string?> SignIn(string email, string password);
}