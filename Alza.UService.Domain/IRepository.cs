namespace Alza.UService.Domain;

/// <summary>
/// A general repository
/// </summary>
public interface IRepository<TEntity, TId>
    where TId : IComparable<TId>
    where TEntity: Entity<TId>
{
    Task<TEntity?> FindById(TId id, CancellationToken cancellationToken = default);
}
