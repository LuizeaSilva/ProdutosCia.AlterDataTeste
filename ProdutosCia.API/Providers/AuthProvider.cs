using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ProdutosCia.API.Providers;

public static class AuthProvider
{
    public static void AddAuthProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(opts =>
            {
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            {
                opts.SaveToken = true;
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asddsnanjsdjnajdnjan jnsdajndjasjndjnsannjnjdjndas")),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = "localhost",
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services
            .AddAuthorization();
    }

    public static IApplicationBuilder UseAuthProvider(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder
            .UseAuthentication()
            .UseAuthorization();
    }
}