using ProdutosCia.Domain.Entities.Base;

namespace ProdutosCia.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : Entity
{
    Task<bool> Exists(Guid id, CancellationToken cancellationToken);
    Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken);
    Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken);
    Task SaveChanges(CancellationToken cancellationToken);
    Task<List<TEntity>> GetAll(CancellationToken cancellationToken);
}