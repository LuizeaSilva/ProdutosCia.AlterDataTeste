using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutosCia.Domain.Entities.Base;

namespace ProdutosCia.Infrastructure.Data.Context.Maps.Base;

public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T : Entity 
{
    public virtual void Configure(EntityTypeBuilder<T> entityTypeBuilder)
    {
        entityTypeBuilder
            .Property(p => p.Id)
            .IsRequired();

        entityTypeBuilder
            .Property(p => p.CreatedAt)
            .IsRequired();

        entityTypeBuilder
            .Property(p => p.UpdatedAt);

        entityTypeBuilder
            .Property(p => p.DeletedAt);

        entityTypeBuilder
            .Property(p => p.Removed)
            .IsRequired();

        entityTypeBuilder
            .ToTable(typeof(T).Name)
            .HasKey(x => x.Id);

        entityTypeBuilder.HasQueryFilter(x => !x.Removed);
    }
}