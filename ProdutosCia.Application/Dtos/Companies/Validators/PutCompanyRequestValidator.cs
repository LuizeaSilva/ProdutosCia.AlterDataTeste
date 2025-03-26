using FluentValidation;
using ProdutosCia.Application.Dtos.Companies.Request;
using ProdutosCia.Domain.Interfaces;

namespace ProdutosCia.Application.Dtos.Companies.Validators;

public class PutCompanyRequestValidator : AbstractValidator<PutCompanyRequest>
{
    private readonly ICompanyRepository _companyRepository;

    public PutCompanyRequestValidator(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull();
    }
}