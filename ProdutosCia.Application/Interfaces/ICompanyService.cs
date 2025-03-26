using ProdutosCia.Application.Dtos.Companies.Request;
using ProdutosCia.Application.Dtos.Companies.Response;

namespace ProdutosCia.Application.Interfaces;

public interface ICompanyService
{
    Task<List<CompanyResponse>> GetAll(CancellationToken cancellationToken);
    Task<CompanyResponse> GetById(Guid id, CancellationToken cancellationToken);
    Task<CompanyResponse> Create(CreateCompanyRequest request, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
    Task Put(Guid id, PutCompanyRequest request, CancellationToken cancellationToken);
    Task<CompanyStockTotalValueResponse> GetStockTotalValue(Guid id, CancellationToken cancellationToken);
    Task<CompanyStockTotalProductResponse> GetStockTotalProduct(Guid id, CancellationToken cancellationToken);
    Task<CompanyStockAverageCostResponse> GetStockAverageCost(Guid id, CancellationToken cancellationToken);
}