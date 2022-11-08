using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TestApp.ModuleIntegration.Processing
{
    public class LogRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LogRequestBehavior<TRequest, TResponse>> _logger;

        public LogRequestBehavior(ILogger<LogRequestBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("Handling {Name}", typeof(TRequest).Name);
        
            var response = await next();
        
            _logger.LogInformation("Handled {Name}", typeof(TRequest).Name);

            return response;
        }
    }
}