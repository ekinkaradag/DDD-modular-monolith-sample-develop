using FluentValidation;

namespace TestApp.BuildingBlocks.Application.Commands
{
    public abstract class CommandValidatorBase<TCommand> : AbstractValidator<TCommand> where TCommand : ICommand
    {
    
    }

    public abstract class CommandWithResultValidatorBase<TCommand, TResult> : AbstractValidator<TCommand> where TCommand : ICommand<TResult>
    {
    
    }
}