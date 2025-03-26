using FluentValidation;
using ProdutosCia.Application.Dtos.Produtcts.Request;
using ProdutosCia.Domain.Interfaces;

namespace ProdutosCia.Application.Dtos.Produtcts.Validators;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    private readonly IProductRepository _productRepository;
    
    public CreateProductRequestValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull();
    }

}