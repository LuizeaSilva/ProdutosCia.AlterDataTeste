

using Microsoft.AspNetCore.Builder;
using ProdutosCia.CrossCutting.IoC.Providers;

namespace ProdutosCia.CrossCutting.IoC;

public static class InjectorContainer
{
    public static void Register(WebApplicationBuilder builder)
    {
        DatabaseContainer.Register(builder.Services, builder.Configuration);
        RepositoriesContainer.Register(builder.Services, builder.Configuration);
        ServicesContainer.Register(builder.Services);
        AutoMapperContainer.Register(builder.Services);
        ValidatorContainer.Register(builder.Services);
    }
}