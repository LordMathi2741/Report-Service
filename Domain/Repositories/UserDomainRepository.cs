using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Model;
using Infrastructure.Repositories;

namespace Domain.Repositories;

public class UserDomainRepository(AppDbContext context) : InfrastructureRepository<User>(context), IUserDomainRepository
{
    public async Task<User> AddAsync(User user)
    {
        await context.Set<User>().AddAsync(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(User user)
    {
        if (await context.Set<User>().FindAsync(user.Id) == null)
        {
            throw new Exception("User not found");
        }
        context.Set<User>().Update(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> DeleteAsync(User user)
    {
        if (await context.Set<User>().FindAsync(user.Id) == null)
        {
            throw new Exception("User not found");
        }
        context.Set<User>().Remove(user);
        await context.SaveChangesAsync();
        return user;
    }
}