using FluentValidation;
using ProdutosCia.Application.Dtos.Produtcts.Request;
using ProdutosCia.Domain.Interfaces;

namespace ProdutosCia.Application.Dtos.Produtcts.Validators;

public class PutProductRequestValidator : AbstractValidator<PutProductRequest>
{
    private readonly IProductRepository _productRepository;

    public PutProductRequestValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull();
    }
}