using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType().Name;

            var requestNameWithGuid = $"{requestName}";

            _logger.LogInformation($"[START] {requestNameWithGuid}");
            TResponse response;

            var stopwatch = Stopwatch.StartNew();
            try
            {
                response = await next();
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation(
                    $"[END] {requestNameWithGuid}; Execution time={stopwatch.ElapsedMilliseconds}ms");
            }

            return response;
        }
    }
}