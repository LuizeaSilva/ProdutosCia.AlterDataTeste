using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutosCia.API.Controllers.Base;
using ProdutosCia.Application.Dtos.Auth.Request;
using ProdutosCia.Application.Interfaces;

namespace ProdutosCia.API.Controllers;

public class AuthController(IAuthService authService) : BaseController
{
    private readonly IAuthService _authService = authService;
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await _authService.Login(request, cancellationToken);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpPost("User")]
    public async Task<IActionResult> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var response = await _authService.CreateUser(request, cancellationToken);
        return Ok(response);
    }
}