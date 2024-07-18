using Infrastructure.Model;

namespace Security.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}