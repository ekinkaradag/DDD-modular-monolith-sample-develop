using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TestApp.BuildingBlocks.Application.Commands;

namespace TestApp.ModuleIntegration.Processing
{
    public class ValidateCommandBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly TypeFilter _commandFilter = (type, criteria) => type == typeof(ICommand) || type == typeof(ICommand<>);
    
        private readonly IList<IValidator<TRequest>> _validators;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var isCommand = request.GetType().FindInterfaces(_commandFilter, null).Any();
        
            if (!isCommand) return await next();

            var errors = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (errors.Any())
            {
                throw new InvalidCommandException(errors.Select(x => x.ErrorMessage).ToList());
            }

            return await next();
        }

        public ValidateCommandBehavior(IList<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
    }
}