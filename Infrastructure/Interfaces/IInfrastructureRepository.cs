namespace Infrastructure.Interfaces;

public interface IInfrastructureRepository<TEntity> where TEntity: class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(long id);
}