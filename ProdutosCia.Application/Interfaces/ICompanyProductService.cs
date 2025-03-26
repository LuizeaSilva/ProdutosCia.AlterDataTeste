using ProdutosCia.Application.Dtos.CompanyProducts.Request;
using ProdutosCia.Application.Dtos.CompanyProducts.Response;

namespace ProdutosCia.Application.Interfaces;

public interface ICompanyProductService
{
    Task<CompanyProductResponse> Create(CreateCompanyProductRequest request, CancellationToken cancellationToken);
    Task IncreaseQuantity(IncreaseQuantityCompanyProductRequest request, CancellationToken cancellationToken);
    Task DecreaseQuantity(DecreaseQuantityCompanyProductRequest request, CancellationToken cancellationToken);
    Task<CompanyProductAverageCostResponse> GetProductAverageCost(Guid id, Guid productId, CancellationToken cancellationToken);
}