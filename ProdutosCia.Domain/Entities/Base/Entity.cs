namespace ProdutosCia.Domain.Entities.Base;

public class Entity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public bool Removed { get; private set; } = false;
    
    public void SetRemoved()
    {
        Removed = true;
        DeletedAt = DateTime.Now;
    }
    
    public void SetUpdated()
    {
        UpdatedAt = DateTime.Now;
    }
}