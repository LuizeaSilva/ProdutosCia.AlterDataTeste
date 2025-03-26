using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ProdutosCia.Infrastructure.Data.Context;

public class ProdutosCiaContext : DbContext
{
    public ProdutosCiaContext(DbContextOptions<ProdutosCiaContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder
            .HasDefaultSchema("dbo")
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}