using System.Threading.Tasks;
using MediatR;
using TestApp.BuildingBlocks.Application.Commands;
using TestApp.BuildingBlocks.Application.Queries;

namespace TestApp.ModuleIntegration.EntryPoint
{
    public class MediatingModuleDispatcher : IModuleDispatcher
    {
        private readonly IMediator _mediator;

        public MediatingModuleDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }
    
        public Task<TResult> Execute<TResult>(ICommand<TResult> command)
        {
            return _mediator.Send(command);
        }

        public Task Execute(ICommand command)
        {
            return _mediator.Send(command);
        }

        public Task<TResult> Execute<TResult>(IQuery<TResult> query)
        {
            return _mediator.Send(query);
        }
    }
}