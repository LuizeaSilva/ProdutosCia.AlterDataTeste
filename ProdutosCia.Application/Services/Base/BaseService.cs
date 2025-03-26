using AutoMapper;

namespace ProdutosCia.Application.Services.Base;

public class BaseService
{
    protected readonly IMapper _mapper;

    public BaseService(IMapper mapper)
    {
        _mapper = mapper;
    }
}