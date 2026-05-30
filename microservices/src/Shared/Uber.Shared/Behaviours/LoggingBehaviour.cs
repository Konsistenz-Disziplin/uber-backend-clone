using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Uber.Shared.Primitives;

namespace Uber.Shared.Behaviours;

public sealed class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken ct)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Handling {RequestName}", requestName);

        var response = await next();

        if (response.IsFailure)
            logger.LogWarning(
                "Request {RequestName} failed with error: {@Error}",
                requestName,
                response.Error);
        else
            logger.LogInformation("Request {RequestName} handled successfully", requestName);

        return response;
    }
}