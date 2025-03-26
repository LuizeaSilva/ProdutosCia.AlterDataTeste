using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutosCia.Domain.Entities;
using ProdutosCia.Infrastructure.Data.Context.Maps.Base;

namespace ProdutosCia.Infrastructure.Data.Context.Maps;

public class CompanyProductMap : BaseMap<CompanyProduct>
{
    public override void Configure(EntityTypeBuilder<CompanyProduct> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.CompanyId)
            .IsRequired();
        
        builder.Property(p => p.ProductId)
            .IsRequired();
        
        builder.Property(p => p.Value)
            .IsRequired();

        builder
            .HasOne(p => p.Company)
            .WithMany(p => p.CompanyProducts)
            .HasForeignKey(p => p.CompanyId);
        
        builder
            .HasOne(p => p.Product)
            .WithMany(p => p.CompanyProducts)
            .HasForeignKey(p => p.ProductId);
    }
}