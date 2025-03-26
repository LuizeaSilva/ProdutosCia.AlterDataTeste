using ProdutosCia.Domain.Entities.Base;

namespace ProdutosCia.Domain.Entities;

public class Company : Entity
{
    public string Name { get; private set; }

    public ICollection<CompanyProduct>? CompanyProducts { get; set; }

    public Company(string name)
    {
        Name = name;
    }

    public double GetStockTotalValue()
    {
        if (CompanyProducts != null)
            return CompanyProducts.Sum(companyProduct => companyProduct.Quantity * companyProduct.Value);
        else
            return 0;
    }
    
    public double GetStockTotalProducts()
    {
        if (CompanyProducts != null)
            return CompanyProducts.Sum(companyProduct => companyProduct.Quantity);
        else
            return 0;
    }
    
    public double GetStockAverageCost()
    {
        return GetStockTotalValue() / GetStockTotalProducts();
    }

    public void Update(string name)
    {
        Name = name;
        SetUpdated();
    }
}