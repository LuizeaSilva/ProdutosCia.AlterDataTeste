using Microsoft.EntityFrameworkCore;
using ProdutosCia.Domain.Entities;
using ProdutosCia.Domain.Interfaces;
using ProdutosCia.Infrastructure.Data.Context;
using ProdutosCia.Infrastructure.Data.Repository.Base;

namespace ProdutosCia.Infrastructure.Data.Repository;

public class UserRepository(ProdutosCiaContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<bool> ValidatedUser(string email, string password, CancellationToken cancellationToken)
    {
        return await _context.Set<User>()
            .AnyAsync(x => x.Email == email && x.Password == password, cancellationToken);
    }
    
    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await _context.Set<User>()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        
    }
}