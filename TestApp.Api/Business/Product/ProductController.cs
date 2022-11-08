using Microsoft.AspNetCore.Mvc;
using TestApp.Module.Product.Application.ApproveProduct;
using TestApp.Module.Product.Application.GetProductOverview;
using TestApp.Module.Product.Application.NewProduct;
using TestApp.Module.Product.Application.DeprecateProduct;
using TestApp.Module.Product.Application.DraftProduct;
using TestApp.ModuleIntegration.EntryPoint;

namespace TestApp.Api.Business.Product
{
    [ApiController]
    public class ProductController
    {
        [HttpGet("/products")]
        public async Task<IEnumerable<ProductOverviewDto>> GetAllProducts([FromServices] IModuleDispatcher moduleDispatcher)
        {
            var query = new GetAllProductsQuery();
            var allProducts = await moduleDispatcher.Execute(query);

            return allProducts;
        }
    
        [HttpPost("/products/")]
        public async Task NewProduct(NewProductCommand command, [FromServices] IModuleDispatcher moduleDispatcher)
        {
            await moduleDispatcher.Execute(command);
        }
        
        [HttpPost("/products/approve")]
        public async Task ApproveProduct(ApproveProductCommand command, [FromServices] IModuleDispatcher moduleDispatcher)
        {
            await moduleDispatcher.Execute(command);
        }
    
        [HttpPost("/products/deprecate")]
        public async Task DeprecateProduct(DeprecateProductCommand command, [FromServices] IModuleDispatcher moduleDispatcher)
        {
            await moduleDispatcher.Execute(command);
        }
        
        [HttpPost("/products/draft")]
        public async Task DraftProduct(DraftProductCommand command, [FromServices] IModuleDispatcher moduleDispatcher)
        {
            await moduleDispatcher.Execute(command);
        }
    }
}