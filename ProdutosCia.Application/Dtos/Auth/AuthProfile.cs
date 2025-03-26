using AutoMapper;
using ProdutosCia.Application.Dtos.Auth.Response;
using ProdutosCia.Domain.Entities;

namespace ProdutosCia.Application.Dtos.Auth;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<User, UserResponse>();
    }
}