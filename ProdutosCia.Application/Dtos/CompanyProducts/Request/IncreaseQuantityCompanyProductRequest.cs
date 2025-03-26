using System.Text.Json.Serialization;

namespace ProdutosCia.Application.Dtos.CompanyProducts.Request;

public class IncreaseQuantityCompanyProductRequest
{
    public int Quantity { get; set; }
    [JsonIgnore]
    public Guid CompanyId { get; set; }
    [JsonIgnore]
    public Guid ProductId { get; set; }
}