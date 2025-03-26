using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProdutosCia.Domain.Interfaces;
using ProdutosCia.Infrastructure.Data.Repository;

namespace ProdutosCia.CrossCutting.IoC.Providers;

internal static class RepositoriesContainer
{
    internal static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICompanyProductRepository, CompanyProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}