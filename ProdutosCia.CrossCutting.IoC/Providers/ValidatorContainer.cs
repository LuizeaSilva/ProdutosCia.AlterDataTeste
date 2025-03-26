using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProdutosCia.Application.Dtos.Companies.Request;
using ProdutosCia.Application.Dtos.Companies.Validators;
using ProdutosCia.Application.Dtos.CompanyProducts.Request;
using ProdutosCia.Application.Dtos.CompanyProducts.Validators;
using ProdutosCia.Application.Dtos.Produtcts.Request;
using ProdutosCia.Application.Dtos.Produtcts.Validators;

namespace ProdutosCia.CrossCutting.IoC.Providers;

internal static class ValidatorContainer
{
    internal static void Register(IServiceCollection services)
    {
        services.AddTransient<IValidator<CreateProductRequest>, CreateProductRequestValidator>();
        services.AddTransient<IValidator<CreateCompanyRequest>, CreateCompanyRequestValidator>();
        services.AddTransient<IValidator<PutProductRequest>, PutProductRequestValidator>();
        services.AddTransient<IValidator<PutCompanyRequest>, PutCompanyRequestValidator>();
        services.AddTransient<IValidator<CreateCompanyProductRequest>, CreateCompanyProductRequestValidator>();
        services.AddTransient<IValidator<IncreaseQuantityCompanyProductRequest>, IncreaseQuantityCompanyProductRequestValidator>();
        services.AddTransient<IValidator<DecreaseQuantityCompanyProductRequest>, DecreaseQuantityCompanyProductRequestValidator>();
    }
}