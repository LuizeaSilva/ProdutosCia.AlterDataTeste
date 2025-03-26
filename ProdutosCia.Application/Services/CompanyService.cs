using AutoMapper;
using FluentValidation;
using ProdutosCia.Application.Dtos.Companies.Request;
using ProdutosCia.Application.Dtos.Companies.Response;
using ProdutosCia.Application.Dtos.CompanyProducts.Request;
using ProdutosCia.Application.Dtos.CompanyProducts.Response;
using ProdutosCia.Application.Interfaces;
using ProdutosCia.Application.Services.Base;
using ProdutosCia.Domain.Entities;
using ProdutosCia.Domain.Exceptions;
using ProdutosCia.Domain.Interfaces;

namespace ProdutosCia.Application.Services;

public class CompanyService(IMapper mapper, 
    ICompanyRepository _companyRepository,
    ICompanyProductRepository _companyProductRepository,
    IProductRepository _productRepository,
    IValidator<CreateCompanyRequest> _createCompanyValidator,
    IValidator<PutCompanyRequest> _putCompanyValidator,
    IValidator<CreateCompanyProductRequest> _createCompanyProductValidator) : BaseService(mapper), ICompanyService
{
    public async Task<List<CompanyResponse>> GetAll(CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.GetAll(cancellationToken);
        return _mapper.Map<List<CompanyResponse>>(companies);
    }
    
    public async Task<CompanyResponse> GetById(Guid id, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetById(id, cancellationToken);
        return _mapper.Map<CompanyResponse>(company);
    }
    
    public async Task<CompanyResponse> Create(CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        await _createCompanyValidator.ValidateAndThrowAsync(request, cancellationToken);
        
        var company = new Company(request.Name);
        var response = await _companyRepository.Create(company, cancellationToken);
        await _companyRepository.SaveChanges(cancellationToken);
        return _mapper.Map<CompanyResponse>(response);
    }
    
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetById(id, cancellationToken);

        if (company == null)
            throw new ObjectNotFoundException("Company not found");
        
        company.SetRemoved();
        await _companyRepository.SaveChanges(cancellationToken);
    }
    
    public async Task Put(Guid id, PutCompanyRequest request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetById(id, cancellationToken);

        if (company == null)
            throw new ObjectNotFoundException("Company not found");
        
        await _putCompanyValidator.ValidateAndThrowAsync(request, cancellationToken);
        
        company.Update(request.Name);
        await _companyRepository.SaveChanges(cancellationToken);
    }
    
    public async Task<CompanyStockTotalValueResponse> GetStockTotalValue(Guid id, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.GetById(id, cancellationToken);
        return _mapper.Map<CompanyStockTotalValueResponse>(companies);
    }
    
    public async Task<CompanyStockTotalProductResponse> GetStockTotalProduct(Guid id, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.GetById(id, cancellationToken);
        return _mapper.Map<CompanyStockTotalProductResponse>(companies);
    }
    
    public async Task<CompanyStockAverageCostResponse> GetStockAverageCost(Guid id, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.GetById(id, cancellationToken);
        return _mapper.Map<CompanyStockAverageCostResponse>(companies);
    }
}