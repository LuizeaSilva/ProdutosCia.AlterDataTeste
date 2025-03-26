using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutosCia.Domain.Entities;
using ProdutosCia.Infrastructure.Data.Context.Maps.Base;

namespace ProdutosCia.Infrastructure.Data.Context.Maps;

public class CompanyMap : BaseMap<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .HasMaxLength(250)
            .IsRequired();
    }
}