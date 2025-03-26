using AutoMapper;
using ProdutosCia.Application.Dtos.CompanyProducts.Response;
using ProdutosCia.Domain.Entities;

namespace ProdutosCia.Application.Dtos.CompanyProducts;

public class CompanyProductProfile : Profile
{
    public CompanyProductProfile()
    {
        CreateMap<CompanyProduct, CompanyProductResponse>();
        CreateMap<CompanyProduct, CompanyProductAverageCostResponse>()
            .ForMember(dest => dest.AverageCost, opts => opts.MapFrom(src => src.GetAverageCost()));
    }
}