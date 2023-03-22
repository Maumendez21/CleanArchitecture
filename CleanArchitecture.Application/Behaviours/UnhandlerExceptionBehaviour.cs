using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Behaviours
{
    public class UnhandlerExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {

        private readonly ILogger<TRequest> _logger;

        public UnhandlerExceptionBehaviour(ILogger<TRequest> logger)
        {
            this._logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception e)
            {

                var requestName = typeof(TRequest).Name;
                _logger.LogError(e, "Application Request: sucedio un aexcepción en el request {Name} {@Request}", requestName, request);
                throw;
            }
        }
    }
}
