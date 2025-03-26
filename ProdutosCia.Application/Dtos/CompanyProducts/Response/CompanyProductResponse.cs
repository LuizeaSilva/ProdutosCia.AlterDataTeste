using ProdutosCia.Application.Dtos.Companies.Response;
using ProdutosCia.Application.Dtos.Produtcts.Response;

namespace ProdutosCia.Application.Dtos.CompanyProducts.Response;

public class CompanyProductResponse
{
    public CompanyResponse Company { get; set; }
    public ProductResponse Product { get; set; }
    public double Value { get; set; }
}