using Microsoft.EntityFrameworkCore;
using ProdutosCia.Domain.Entities;
using ProdutosCia.Domain.Interfaces;
using ProdutosCia.Infrastructure.Data.Context;
using ProdutosCia.Infrastructure.Data.Repository.Base;

namespace ProdutosCia.Infrastructure.Data.Repository;

public class CompanyRepository(ProdutosCiaContext context) : BaseRepository<Company>(context), ICompanyRepository
{
    public override async Task<Company?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<Company>()
            .Include(x => x.CompanyProducts)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}