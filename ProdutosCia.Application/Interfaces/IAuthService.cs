using ProdutosCia.Application.Dtos.Auth.Request;
using ProdutosCia.Application.Dtos.Auth.Response;

namespace ProdutosCia.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginRequest request, CancellationToken cancellationToken);
    Task<UserResponse> CreateUser(CreateUserRequest request, CancellationToken cancellationToken);
}