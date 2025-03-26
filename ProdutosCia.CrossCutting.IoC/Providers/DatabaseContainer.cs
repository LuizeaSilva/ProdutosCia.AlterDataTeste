using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProdutosCia.Infrastructure.Data.Context;

namespace ProdutosCia.CrossCutting.IoC.Providers;

internal static class DatabaseContainer
{
    internal static void Register(IServiceCollection services, IConfiguration configuration)
    {
        UseSqlDatabase(services, configuration);
    }

    private static void UseSqlDatabase(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ProdutosCiaContext>(dbOptions =>
        {
            dbOptions
                .UseSqlServer(connectionString, x =>
                {
                    x.MigrationsAssembly("ProdutosCia.Infrastructure.Data.Migrations");
                });
        });
    }
}