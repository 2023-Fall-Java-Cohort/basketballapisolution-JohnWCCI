
namespace BasketballAPI.Services
{
    public interface IService<TEntity> where TEntity : class
    {
        ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken cancellation = default);
        ValueTask<bool> DeleteAsync(TEntity entity, CancellationToken cancellation = default);
        ValueTask<List<TEntity>> GetAllAsync(CancellationToken cancellation = default);
        ValueTask<TEntity?> GetAsync(int id, CancellationToken cancellation = default);
        ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellation = default);
    }
}