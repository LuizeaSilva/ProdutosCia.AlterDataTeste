using Microsoft.Extensions.DependencyInjection;
using ProdutosCia.Application.Interfaces;
using ProdutosCia.Application.Services;

namespace ProdutosCia.CrossCutting.IoC.Providers;

internal static class ServicesContainer
{
    internal static void Register(IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICompanyProductService, CompanyProductService>();
        services.AddScoped<IAuthService, AuthService>();
    }
}