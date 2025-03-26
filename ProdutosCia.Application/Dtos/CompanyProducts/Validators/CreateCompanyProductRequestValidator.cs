using FluentValidation;
using ProdutosCia.Application.Dtos.CompanyProducts.Request;
using ProdutosCia.Domain.Interfaces;

namespace ProdutosCia.Application.Dtos.CompanyProducts.Validators;

public class CreateCompanyProductRequestValidator : AbstractValidator<CreateCompanyProductRequest>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICompanyProductRepository _companyProductRepository;
    
    public CreateCompanyProductRequestValidator(ICompanyRepository companyRepository, IProductRepository productRepository, ICompanyProductRepository companyProductRepository)
    {
        _companyRepository = companyRepository;
        _productRepository = productRepository;
        _companyProductRepository = companyProductRepository;

        RuleFor(x => x.Value)
            .GreaterThan(0);
        
        RuleFor(x => x.CompanyId)
            .MustAsync(ExistCompany).WithMessage("Company doesn't exist");
        
        RuleFor(x => x.ProductId)
            .MustAsync(ExistProduct).WithMessage("Product doesn't exist");
        
        RuleFor(x => x)
            .MustAsync(NotExistCompanyProduct).WithMessage("Product already linked to this company");
    }

    private async Task<bool> ExistCompany(Guid id, CancellationToken cancellationToken)
    {
        return await _companyRepository.Exists(id, cancellationToken);
    }

    private async Task<bool> ExistProduct(Guid id, CancellationToken cancellationToken)
    {
        return await _productRepository.Exists(id, cancellationToken);
    }

    private async Task<bool> NotExistCompanyProduct(CreateCompanyProductRequest request, CancellationToken cancellationToken)
    {
        var companyProduct = await _companyProductRepository.GetByCompanyAndProduct(request.CompanyId, request.ProductId, cancellationToken);
        return companyProduct == null;
    }
}