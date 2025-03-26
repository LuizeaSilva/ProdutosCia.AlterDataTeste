using ProdutosCia.Domain.Entities;

namespace ProdutosCia.Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> ValidatedUser(string email, string password, CancellationToken cancellationToken);
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
}