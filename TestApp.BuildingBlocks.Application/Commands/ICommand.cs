using MediatR;

namespace TestApp.BuildingBlocks.Application.Commands
{
    /// <summary>
    /// A change request
    /// Every command is handled as a single transaction
    /// </summary>
    public interface ICommand : IRequest
    {
    }

    /// <summary>
    /// A change request
    /// Every command is handled as a single transaction
    /// </summary>
    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}