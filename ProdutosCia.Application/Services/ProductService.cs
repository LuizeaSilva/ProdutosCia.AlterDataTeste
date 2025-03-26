using AutoMapper;
using FluentValidation;
using ProdutosCia.Application.Dtos;
using ProdutosCia.Application.Dtos.Produtcts.Request;
using ProdutosCia.Application.Dtos.Produtcts.Response;
using ProdutosCia.Application.Interfaces;
using ProdutosCia.Application.Services.Base;
using ProdutosCia.Domain.Entities;
using ProdutosCia.Domain.Exceptions;
using ProdutosCia.Domain.Interfaces;

namespace ProdutosCia.Application.Services;

public class ProductService(IMapper mapper, 
    IProductRepository _productRepository,
    IValidator<CreateProductRequest> _createProductValidator,
    IValidator<PutProductRequest> _putProductValidator) : BaseService(mapper), IProductService
{
    public async Task<List<ProductResponse>> GetAll(CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAll(cancellationToken);
        return _mapper.Map<List<ProductResponse>>(products);
    }
    
    public async Task<ProductResponse> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(id, cancellationToken);
        return _mapper.Map<ProductResponse>(product);
    }
    
    public async Task<ProductResponse> Create(CreateProductRequest request, CancellationToken cancellationToken)
    {
        await _createProductValidator.ValidateAndThrowAsync(request, cancellationToken);
        
        var product = new Product(request.Name);
        var response = await _productRepository.Create(product, cancellationToken);
        await _productRepository.SaveChanges(cancellationToken);
        return _mapper.Map<ProductResponse>(response);
    }
    
    public async Task<List<BulkCreateResponse>> BulkCreate(List<CreateProductRequest> requests, CancellationToken cancellationToken)
    {
        var response = new List<BulkCreateResponse>();
        
        foreach (var request in requests)
        {
            var validate = await _createProductValidator.ValidateAsync(request, cancellationToken);

            if (!validate.IsValid)
            {
                var erros = validate.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Select(x => x.ErrorMessage.Replace("'", "")).ToArray());
                
                response.Add(new BulkCreateResponse()
                {
                    Item = request,
                    StatusCode = 400,
                    Erros = erros
                });
                
                continue;
            }

            var product = new Product(request.Name);
            var productResponse = await _productRepository.Create(product, cancellationToken);
            await _productRepository.SaveChanges(cancellationToken);
            
            response.Add(new BulkCreateResponse()
            {
                Item = _mapper.Map<ProductResponse>(productResponse),
                StatusCode = 200
            });
        }

        return response;
    }
    
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(id, cancellationToken);

        if (product == null)
            throw new ObjectNotFoundException("Product not found");
        
        product.SetRemoved();
        await _productRepository.SaveChanges(cancellationToken);
    }
    
    public async Task Put(Guid id, PutProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(id, cancellationToken);

        if (product == null)
            throw new ObjectNotFoundException("{Product not found");
        
        await _putProductValidator.ValidateAndThrowAsync(request, cancellationToken);
        
        product.Update(request.Name);
        await _productRepository.SaveChanges(cancellationToken);
    }
}