using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestApp.BuildingBlocks.Application;
using TestApp.BuildingBlocks.Application.Commands;
using TestApp.Module.Product.Domain.Product;

namespace TestApp.Module.Product.Application.DeprecateProduct
{
    internal class DeprecateProductCommandHandler : ICommandHandler<DeprecateProductCommand>
    {
        private readonly IProductRepository _repository;

        public DeprecateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }
    
        public async Task<Unit> Handle(DeprecateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(command.ProductId);
        
            if (product == null) throw new NotFoundException("Product not found");

            product.Deprecate();

            await _repository.CommitAsync();

            return Unit.Value;
        }
    }
}