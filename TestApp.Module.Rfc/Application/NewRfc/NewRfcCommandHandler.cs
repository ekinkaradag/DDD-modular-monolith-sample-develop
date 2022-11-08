using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestApp.BuildingBlocks.Application.Commands;
using TestApp.Module.Rfc.Domain.RequestForChange;

namespace TestApp.Module.Rfc.Application.NewRfc
{
    internal class NewRfcCommandHandler : ICommandHandler<NewRfcCommand>
    {
        private readonly IRfcRepository _repository;

        public NewRfcCommandHandler(IRfcRepository repository)
        {
            _repository = repository;
        }
    
        public async Task<Unit> Handle(NewRfcCommand command, CancellationToken cancellationToken)
        {
            var requestForChange = RequestForChange.Create(command.Key!, command.Title!);

            requestForChange.Start();

            await _repository.AddAsync(requestForChange);

            return Unit.Value;
        }
    }
}