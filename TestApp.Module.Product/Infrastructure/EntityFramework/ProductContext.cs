using Microsoft.EntityFrameworkCore;

namespace TestApp.Module.Product.Infrastructure.EntityFramework
{
    internal class ProductContext : DbContext
    {
        public DbSet<Product.Domain.Product.Product> Products { get; set; } = null!;

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}