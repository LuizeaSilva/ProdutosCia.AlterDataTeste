using AutoMapper;
using ProdutosCia.Application.Dtos.Companies.Response;
using ProdutosCia.Domain.Entities;

namespace ProdutosCia.Application.Dtos.Companies;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyResponse>();
        CreateMap<Company, CompanyStockTotalValueResponse>()
            .ForMember(dest => dest.TotalValue, opts => opts.MapFrom(src => src.GetStockTotalValue()));
        CreateMap<Company, CompanyStockTotalProductResponse>()
            .ForMember(dest => dest.TotalProducts, opts => opts.MapFrom(src => src.GetStockTotalProducts()));
        CreateMap<Company, CompanyStockAverageCostResponse>()
            .ForMember(dest => dest.AverageCost, opts => opts.MapFrom(src => src.GetStockAverageCost()));
    }
}