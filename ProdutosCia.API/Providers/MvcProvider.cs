using System.Globalization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ProdutosCia.API.Providers;

public static class MvcProvider
{
    public static void AddMvcProvider(this IServiceCollection services)
    {
        var cultureInfo = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        
        services.AddCors(p => p.AddPolicy("corsApp", builder =>
        {
            builder
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
        
        services
            .AddControllers();

        services
            .AddMvc(opts =>
            {
                //opts.Filters.Add(typeof(ValidateModelStateAttribute));
                //opts.ModelValidatorProviders.Clear();
            })
            .AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
             
            });
        
        services.Configure<ApiBehaviorOptions>(opts => { opts.SuppressModelStateInvalidFilter = true; });

        services.AddEndpointsApiExplorer();
    }

    public static void UseMvcProvider(this IApplicationBuilder app)
    {
        app
            .UseCors("corsApp");

        app
            .UseRouting()
            .UseStaticFiles()
            .UseHttpsRedirection()
            .UseAuthProvider()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
    }
}