using IoCore.SharedReadKernel.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoCore.SharedReadKernel.Product.EntityConfigurations
{
    internal class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        protected override void ConfigureCore(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", "dbo");
        }
    }
}