
using MediatR;
using Microsoft.Extensions.Logging;

namespace JiraSample.Command.Application.Common.Behaviors;

public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    private readonly ILogger<TRequest> _logger;

    public ExceptionHandlingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        try
        {
            return await next();
        }
        catch (AggregateException ex)
        {
            _logger.LogError(ex, $"Unhandled Exception for Request {requestName} {request}");
            foreach (Exception exception in ex.InnerExceptions)
            {
                _logger.LogError(exception, $"Unhandled Inner Exception for Request {requestName} {request}");
            }
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unhandled Exception for Request {requestName} {request}");
            throw;
        }
    }
}