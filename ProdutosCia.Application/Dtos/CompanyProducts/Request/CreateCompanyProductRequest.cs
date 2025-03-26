using System.Text.Json.Serialization;

namespace ProdutosCia.Application.Dtos.CompanyProducts.Request;

public class CreateCompanyProductRequest
{
    [JsonIgnore]
    public Guid CompanyId { get; set; }
    [JsonIgnore]
    public Guid ProductId { get; set; }
    public double Value { get; set; }
}