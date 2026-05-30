using FluentValidation;
using MediatR;
using Uber.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Text;
using Uber.Shared.Primitives;

namespace Uber.Shared.Behaviours;

public sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken ct)
    {
        if (!validators.Any())
            return await next();

        var errors = validators
            .Select(v => v.Validate(request))
            .Where(r => !r.IsValid)
            .SelectMany(r => r.Errors)
            .Select(f => new Error(f.PropertyName, f.ErrorMessage))
            .ToArray();

        if (errors.Length != 0)
        {
            return (TResponse)typeof(Result)
                .GetMethod(nameof(Result.Failure))!
                .MakeGenericMethod(typeof(TResponse).GenericTypeArguments[0])
                .Invoke(null, [new ValidationError(errors)])!;
        }

        return await next();
    }
}
