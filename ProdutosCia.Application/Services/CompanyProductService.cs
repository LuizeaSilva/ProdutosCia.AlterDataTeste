using AutoMapper;
using FluentValidation;
using ProdutosCia.Application.Dtos.CompanyProducts.Request;
using ProdutosCia.Application.Dtos.CompanyProducts.Response;
using ProdutosCia.Application.Interfaces;
using ProdutosCia.Application.Services.Base;
using ProdutosCia.Domain.Entities;
using ProdutosCia.Domain.Interfaces;

namespace ProdutosCia.Application.Services;

public class CompanyProductService(IMapper mapper, 
    ICompanyRepository _companyRepository,
    ICompanyProductRepository _companyProductRepository,
    IProductRepository _productRepository,
    IValidator<CreateCompanyProductRequest> _createCompanyProductValidator,
    IValidator<IncreaseQuantityCompanyProductRequest> _increaseQuantityCompanyProductRequest,
    IValidator<DecreaseQuantityCompanyProductRequest> _decreaseQuantityCompanyProductRequest) : BaseService(mapper), ICompanyProductService
{
    public async Task<CompanyProductResponse> Create(CreateCompanyProductRequest request, CancellationToken cancellationToken)
    {
        await _createCompanyProductValidator.ValidateAndThrowAsync(request, cancellationToken);

        var company = await _companyRepository.GetById(request.CompanyId, cancellationToken);
        var product = await _productRepository.GetById(request.ProductId, cancellationToken);
        
        var companyProduct = new CompanyProduct(request.CompanyId, request.ProductId, request.Value);
        
        var response = await _companyProductRepository.Create(companyProduct, cancellationToken);
        await _companyRepository.SaveChanges(cancellationToken);
        return _mapper.Map<CompanyProductResponse>(response);
    }
    
    public async Task IncreaseQuantity(IncreaseQuantityCompanyProductRequest request, CancellationToken cancellationToken)
    {
        await _increaseQuantityCompanyProductRequest.ValidateAndThrowAsync(request, cancellationToken);
        
        var companyProduct = await _companyProductRepository.GetByCompanyAndProduct(request.CompanyId, request.ProductId, cancellationToken);
        companyProduct!.IncreaseQuantity(request.Quantity);
        await _companyProductRepository.SaveChanges(cancellationToken);
    }
    
    public async Task DecreaseQuantity(DecreaseQuantityCompanyProductRequest request, CancellationToken cancellationToken)
    {
        await _decreaseQuantityCompanyProductRequest.ValidateAndThrowAsync(request, cancellationToken);
        
        var companyProduct = await _companyProductRepository.GetByCompanyAndProduct(request.CompanyId, request.ProductId, cancellationToken);
        companyProduct!.DecreaseQuantity(request.Quantity);
        await _companyProductRepository.SaveChanges(cancellationToken);
    }
    
    public async Task<CompanyProductAverageCostResponse> GetProductAverageCost(Guid id, Guid productId, CancellationToken cancellationToken)
    {
        var companies = await _companyProductRepository.GetByCompanyAndProduct(id, productId, cancellationToken);
        return _mapper.Map<CompanyProductAverageCostResponse>(companies);
    }
}