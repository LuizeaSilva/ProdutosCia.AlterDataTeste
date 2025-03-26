using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProdutosCia.API.Providers;

public static class SwaggerProvider
{
    private static string _mainAssembly = $"ProdutosCia.Api";

    public static void AddSwaggerProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(opts =>
        {
            opts.SwaggerDoc("v1", GetApiInfo("v1", configuration["BUILD_NUMBER"] ?? "localhost"));

            opts.UseAuthorization();
            opts.MapCustomTypes();
        });

        services.AddFluentValidationRulesToSwagger();
    }

    private static void MapCustomTypes(this SwaggerGenOptions opts)
    {
        opts.MapType<DateOnly>(() => new OpenApiSchema
        {
            Type = "string",
            Format = "date"
        });
    }

    public static void UseSwaggerProvider(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder
            .UseSwagger()
            .UseSwaggerUI();
    }

    private static OpenApiInfo GetApiInfo(string version, string buildNumber)
    {
        return new OpenApiInfo
        {
            Version = version,
            Title = $"{_mainAssembly}",
            Description = $"<i>Build: <b>{buildNumber}</b></i>"
        };
    }

    private static void UseAuthorization(this SwaggerGenOptions opts)
    {
        opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n",
            Name = "Authorization",
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        });

        opts.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },

                Array.Empty<string>()
            }
        });
    }
}