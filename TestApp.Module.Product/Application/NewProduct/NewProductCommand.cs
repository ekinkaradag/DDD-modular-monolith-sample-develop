using TestApp.BuildingBlocks.Application.Commands;

namespace TestApp.Module.Product.Application.NewProduct
{
    public record NewProductCommand : ICommand
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
    }
}