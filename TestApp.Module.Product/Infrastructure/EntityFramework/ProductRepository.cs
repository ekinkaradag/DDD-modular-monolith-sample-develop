using System;
using System.Threading.Tasks;
using TestApp.Module.Product.Domain.Product;

namespace TestApp.Module.Product.Infrastructure.EntityFramework
{
    internal class ProductRepository : IProductRepository
    {
        private readonly ProductContext _dbContext;

        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Domain.Product.Product product)
        {
            await _dbContext.Products.AddAsync(product);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Domain.Product.Product> GetByIdAsync(Guid id)
        {
            var rfc = await _dbContext.Products.FindAsync(id);

            return rfc;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}