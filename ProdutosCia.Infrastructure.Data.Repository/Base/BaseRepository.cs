using Microsoft.EntityFrameworkCore;
using ProdutosCia.Domain.Entities.Base;
using ProdutosCia.Domain.Interfaces;
using ProdutosCia.Infrastructure.Data.Context;

namespace ProdutosCia.Infrastructure.Data.Repository.Base;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
{
    protected readonly ProdutosCiaContext _context;

    protected BaseRepository(ProdutosCiaContext context)
    {
        _context = context;
    }
    
    public virtual async Task<List<TEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().ToListAsync(cancellationToken);
    }
    
    public virtual async Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<bool> Exists(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>()
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
     
    public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken)
    {
        var result = await _context.Set<TEntity>()
            .AddAsync(entity);

        return result.Entity;
    }

    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}