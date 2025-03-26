using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using ProdutosCia.Application.Dtos.Auth.Request;
using ProdutosCia.Application.Dtos.Auth.Response;
using ProdutosCia.Application.Interfaces;
using ProdutosCia.Application.Services.Base;
using ProdutosCia.CrossCutting.Commons.Extensions;
using ProdutosCia.Domain.Entities;
using ProdutosCia.Domain.Exceptions;
using ProdutosCia.Domain.Interfaces;

namespace ProdutosCia.Application.Services;

public class AuthService(IMapper mapper, IUserRepository _userRepository) : BaseService(mapper), IAuthService
{
    public async Task<LoginResponse> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ValidatedUser(request.Email, request.Password.ToSha512(), cancellationToken))
            throw new UnauthorizedException("user or password incorrect!");

        var user = await _userRepository.GetByEmail(request.Email, cancellationToken);
        var token = new LoginResponse()
        {
            Token = GenerateToken(user)
        };
        
        return token;
    }
    
    private string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asddsnanjsdjnajdnjan jnsdajndjasjndjnsannjnjdjndas"));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        
        var identity = new ClaimsIdentity(
            new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            }
        );

        var expireDate = DateTime.UtcNow.AddMinutes(25);
        var createDate = DateTime.Now;

        var handler = new JwtSecurityTokenHandler();
        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = "localhost",
            SigningCredentials = signingCredentials,
            Subject = identity,
            NotBefore = createDate,
            Expires = expireDate
        });
        var token = handler.WriteToken(securityToken);

        return token;
    } 
    
    public async Task<UserResponse> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        //await _createCompanyValidator.ValidateAndThrowAsync(request, cancellationToken);
        
        var user = new User(request.Name, request.Email, request.Password.ToSha512());
        var response = await _userRepository.Create(user, cancellationToken);
        await _userRepository.SaveChanges(cancellationToken);
        return _mapper.Map<UserResponse>(response);
    }
}