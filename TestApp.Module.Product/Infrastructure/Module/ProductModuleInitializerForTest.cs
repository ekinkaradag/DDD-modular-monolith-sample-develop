using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using TestApp.BuildingBlocks.Module;
using TestApp.Module.Product.Infrastructure.EntityFramework;

namespace TestApp.Module.Product.Infrastructure.Module
{
    internal class ProductModuleInitializerForTest : IModuleInitializer
    {
        private readonly ProductContext _context;

        public ProductModuleInitializerForTest(ProductContext context)
        {
            _context = context;
        }

        public async Task<bool> Run(bool databaseAlreadyCleared)
        {
            if(!databaseAlreadyCleared)
                await _context.Database.EnsureDeletedAsync();
            var created = await _context.Database.EnsureCreatedAsync();
            if (!created)
            {
                var databaseCreator = 
                    (RelationalDatabaseCreator) _context.Database.GetService<IDatabaseCreator>();
                await databaseCreator.CreateTablesAsync();
            }
            

            // They all start as "Deprecated"
            var first = Domain.Product.Product.Create("PRD-1", "Product 1", "1.0");
            var second = Domain.Product.Product.Create("PRD-2", "Product 2", "2.0");
            var third = Domain.Product.Product.Create("PRD-3", "Product 3", "3.0");

            second.Approve();
            third.Approve();
            third.Deprecate();

            await _context.Products.AddRangeAsync(first, second, third);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}