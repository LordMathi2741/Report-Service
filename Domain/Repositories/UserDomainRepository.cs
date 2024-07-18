using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Model;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Security.Interfaces;

namespace Domain.Repositories;

public class UserDomainRepository(AppDbContext context, IUnitOfWork unitOfWork,IEncryptService encryptService, ITokenService tokenService) : InfrastructureRepository<User>(context), IUserDomainRepository
{
    private const string Symbols = "@#$%^&*()_+";
    private bool _containsSymbol = false;
    public async Task<User> SignUp(User user)
    {
        foreach (var symbol in Symbols.Where(symbol => user.Password.Contains(symbol)))
        {
            _containsSymbol = true;
            break;
        }
        if (await context.Set<User>().AnyAsync(userFound => userFound.Email == user.Email))
        {
            throw new Exception("User with this email or password already exists");
        }
        if (!user.Email.Contains("@"))
        {
            throw new Exception("Email must be valid");
        }

        if (user.Ruc.Length != 11 || !user.Ruc.All(char.IsDigit))
        {
            throw new Exception("Ruc must be 11 characters long and contain only numbers");
        }

        if (user.Password.Length < 8 || !_containsSymbol || user.Password == "12345678")
        {
            throw new Exception("Password must be at least 8 characters long and contain one symbol  and not be 12345678");
        }
        var userHashed = new User()
        {
            Username = user.Username,
            Role = user.Role,
            Password = encryptService.Encrypt(user.Password),
            Email = user.Email,
            Ruc = user.Ruc,
            SocialReason = user.SocialReason,
            CreatedDate = DateTimeOffset.UtcNow

        };

        await context.Set<User>().AddAsync(userHashed);
        await unitOfWork.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(User user)
    {
        if (!Enum.IsDefined(typeof(ERoleTypes), user.Role))
        {
            throw new Exception("Role must be valid");
        }
        if (await context.Set<User>().FindAsync(user.Id) == null)
        {
            throw new Exception("User not found");
        }

        context.Set<User>().Update(user);
        await unitOfWork.SaveChangesAsync();
        return user;
    }

    public async Task<User?> DeleteAsync(User user)
    {
        context.Set<User>().Remove(user);
        await unitOfWork.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUsernameByEmail(string email)
    {
        return await context.Set<User>().FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<string?> GetUserRoleByUsername(string username)
    {
        return await context.Set<User>().Where(user => user.Username == username).Select(user => user.Role).FirstOrDefaultAsync();
    }

    public async Task<string?>SignIn(string email, string password)
    {
        var user = await context.Set<User>().FirstOrDefaultAsync(userFound => userFound.Email == email);
        if (user != null)
        {
            if (encryptService.VerifyPassword(password, user.Password))
            {
                var token = tokenService.GenerateToken(user);
        
                return token;
            }
        }
        return null;
    }
    
}