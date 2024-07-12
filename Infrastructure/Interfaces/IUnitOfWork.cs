namespace Infrastructure.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}