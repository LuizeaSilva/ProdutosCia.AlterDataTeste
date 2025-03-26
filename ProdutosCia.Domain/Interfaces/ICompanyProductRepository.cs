using ProdutosCia.Domain.Entities;

namespace ProdutosCia.Domain.Interfaces;

public interface ICompanyProductRepository : IBaseRepository<CompanyProduct>
{
    Task<CompanyProduct?> GetByCompanyAndProduct(Guid companyId, Guid productId, CancellationToken cancellationToken);
}