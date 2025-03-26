using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutosCia.Domain.Entities;
using ProdutosCia.Infrastructure.Data.Context.Maps.Base;

namespace ProdutosCia.Infrastructure.Data.Context.Maps;

public class UserMap : BaseMap<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(p => p.Name)
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(p => p.Password)
            .HasMaxLength(250)
            .IsRequired();
    }
}