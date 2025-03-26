using AutoMapper;
using ProdutosCia.Application.Dtos.Produtcts.Response;
using ProdutosCia.Domain.Entities;

namespace ProdutosCia.Application.Dtos.Produtcts;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>();
    }
    
}