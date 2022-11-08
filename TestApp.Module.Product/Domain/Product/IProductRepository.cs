using System.Threading.Tasks;
using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Product.Domain.Product
{
    internal interface IProductRepository : IRepository<Product>
    {
        Task CommitAsync();
    }
}