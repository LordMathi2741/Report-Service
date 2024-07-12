using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Model;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories;

public class UserDomainRepository(AppDbContext context, IUnitOfWork unitOfWork) : InfrastructureRepository<User>(context), IUserDomainRepository
{
    public async Task<User> AddAsync(User user)
    {
        if (await context.Set<User>().AnyAsync(userFound => userFound.Email == user.Email))
        {
            throw new Exception("User with this email or password already exists");
        }
        if (!user.Email.EndsWith("@gmail.com") && !user.Email.EndsWith("@outlook.es"))
        {
            throw new Exception("Email must be valid");
        }

        if (user.Password.Length < 8 || !user.Password.Contains("@") || user.Password == "12345678")
        {
            throw new Exception("Password must be at least 8 characters long and contain @ symbol and not be 12345678");
        }
        await context.Set<User>().AddAsync(user);
        await unitOfWork.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(long id,User user)
    {
        if (await context.Set<User>().FindAsync(id) == null)
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

    public async Task<User?> GetUserByEmailAndPassword(string email, string password)
    {
        return await context.Set<User>().Where(userFound => userFound.Email == email && userFound.Password == password).FirstOrDefaultAsync();
    }
    
}