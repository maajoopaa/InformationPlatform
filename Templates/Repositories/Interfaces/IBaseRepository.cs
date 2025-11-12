using System.Linq.Expressions;

namespace Templates.Repositories.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseDbEntityWithId
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteRangeAsync(List<TEntity> entities, CancellationToken cancellationToken);
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}