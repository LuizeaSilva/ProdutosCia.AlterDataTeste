using ProdutosCia.Application.Dtos;
using ProdutosCia.Application.Dtos.Produtcts.Request;
using ProdutosCia.Application.Dtos.Produtcts.Response;

namespace ProdutosCia.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductResponse>> GetAll(CancellationToken cancellationToken);
    Task<ProductResponse> GetById(Guid id, CancellationToken cancellationToken);
    Task<ProductResponse> Create(CreateProductRequest request, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
    Task Put(Guid id, PutProductRequest request, CancellationToken cancellationToken);
    Task<List<BulkCreateResponse>> BulkCreate(List<CreateProductRequest> requests, CancellationToken cancellationToken);
}