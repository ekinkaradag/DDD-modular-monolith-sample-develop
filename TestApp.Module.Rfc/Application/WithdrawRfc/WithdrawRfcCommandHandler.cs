using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestApp.BuildingBlocks.Application;
using TestApp.BuildingBlocks.Application.Commands;
using TestApp.Module.Rfc.Domain.RequestForChange;

namespace TestApp.Module.Rfc.Application.WithdrawRfc
{
    internal class WithdrawRfcCommandHandler : ICommandHandler<WithdrawRfcCommand>
    {
        private readonly IRfcRepository _repository;

        public WithdrawRfcCommandHandler(IRfcRepository repository)
        {
            _repository = repository;
        }
    
        public async Task<Unit> Handle(WithdrawRfcCommand command, CancellationToken cancellationToken)
        {
            var requestForChange = await _repository.GetByIdAsync(command.RfcId);
        
            if (requestForChange == null) throw new NotFoundException("Request For Change not found");

            requestForChange.WithDraw();

            await _repository.CommitAsync();

            return Unit.Value;
        }
    }
}