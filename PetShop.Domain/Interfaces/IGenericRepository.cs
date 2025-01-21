namespace PetShop.Domain.Interfaces;

public interface IGenericRepository<TEntity, TKey> where TEntity : class,IEntity<TKey>
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>?> GetAllAsync();
    Task<TKey> InsertAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
}