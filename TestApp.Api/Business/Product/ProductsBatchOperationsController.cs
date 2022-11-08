using Microsoft.AspNetCore.Mvc;
using TestApp.Module.Product.Application.GetProductOverview;
using TestApp.ModuleIntegration.EntryPoint;

namespace TestApp.Api.Business.Product
{
    [ApiController]
    public class ProductsBatchOperationsController
    {
        [HttpGet("/products/load-grid")]
        public async Task<IEnumerable<ProductOverviewDto>> GetProductOverviewGridData([FromServices] IModuleDispatcher moduleDispatcher)
        {
            var query = new GetAllProductsQuery();
            var allProducts = await moduleDispatcher.Execute(query);

            return allProducts;
        }
    }
}