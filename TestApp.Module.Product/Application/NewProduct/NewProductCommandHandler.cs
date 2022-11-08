using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestApp.BuildingBlocks.Application.Commands;
using TestApp.Module.Product.Domain.Product;

namespace TestApp.Module.Product.Application.NewProduct
{
    internal class NewProductCommandHandler : ICommandHandler<NewProductCommand>
    {
        private readonly IProductRepository _repository;

        public NewProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }
    
        public async Task<Unit> Handle(NewProductCommand command, CancellationToken cancellationToken)
        {
            var product = Domain.Product.Product.Create(command.Key!, command.Title!, command.Version!);

            await _repository.AddAsync(product);

            return Unit.Value;
        }
    }
}