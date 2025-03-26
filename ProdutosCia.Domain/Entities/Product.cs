using ProdutosCia.Domain.Entities.Base;

namespace ProdutosCia.Domain.Entities;

public class Product : Entity
{
    public string Name { get; private set; }
    public ICollection<CompanyProduct>? CompanyProducts { get; set; }

    public Product(string name)
    {
        Name = name;
    }
    
    public void Update(string name)
    {
        Name = name;
        SetUpdated();
    }
}