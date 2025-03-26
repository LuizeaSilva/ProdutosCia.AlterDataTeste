using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ProdutosCia.CrossCutting.IoC.Providers;

internal static class AutoMapperContainer
{
    internal static void Register(IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.ConstructServicesUsing(services.BuildServiceProvider().GetRequiredService);
        }, Assembly.Load("ProdutosCia.Application"));
    }
}