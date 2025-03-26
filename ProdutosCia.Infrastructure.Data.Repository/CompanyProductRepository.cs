using Microsoft.EntityFrameworkCore;
using ProdutosCia.Domain.Entities;
using ProdutosCia.Domain.Interfaces;
using ProdutosCia.Infrastructure.Data.Context;
using ProdutosCia.Infrastructure.Data.Repository.Base;

namespace ProdutosCia.Infrastructure.Data.Repository;

public class CompanyProductRepository(ProdutosCiaContext context) : BaseRepository<CompanyProduct>(context), ICompanyProductRepository
{
    public async Task<CompanyProduct?> GetByCompanyAndProduct(Guid companyId, Guid productId, CancellationToken cancellationToken)
    {
        return await _context.Set<CompanyProduct>().FirstOrDefaultAsync(x => x.CompanyId == companyId &&
            x.ProductId == productId, cancellationToken);
    }
}