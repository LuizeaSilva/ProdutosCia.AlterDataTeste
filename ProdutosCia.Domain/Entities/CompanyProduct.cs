using ProdutosCia.Domain.Entities.Base;

namespace ProdutosCia.Domain.Entities;

public class CompanyProduct : Entity
{
    public Guid CompanyId { get; private set; }
    public Company? Company { get; private set; }

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public int Quantity { get; set; }
    public double Value { get; set; }

    public CompanyProduct(Guid companyId, Guid productId, double value)
    {
        CompanyId = companyId;
        ProductId = productId;
        Value = value;
        Quantity = 0;
    }
    
    public double GetAverageCost()
    {
        return Value / Quantity;
    }

    public void IncreaseQuantity(int quantity)
    {
        Quantity += quantity;
        SetUpdated();
    }

    public void DecreaseQuantity(int quantity)
    {
        Quantity -= quantity;
        SetUpdated();
    }
}