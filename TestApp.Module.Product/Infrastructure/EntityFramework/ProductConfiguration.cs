using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApp.BuildingBlocks.Infrastructure.Domain.EntityFramework;

namespace TestApp.Module.Product.Infrastructure.EntityFramework
{
    internal class ProductConfiguration : EntityTypeConfiguration<Domain.Product.Product>
    {
        protected override void ConfigureCore(EntityTypeBuilder<Domain.Product.Product> builder)
        {
            builder.ToTable("Product");
            builder.HasAlternateKey(product => product.Key);
            builder.Property(product => product.Title).IsRequired();
            builder.Property(product => product.Version).IsRequired();
            builder.Property(product => product.Status).IsRequired();
        }
    }
}