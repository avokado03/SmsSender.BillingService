using FluentValidation;
using MediatR;

namespace SmsSender.BillingService.CQRS.Bootstrap.Behaviors;

/// <summary>
/// Осуществляет валидацию данных запроса.
/// </summary>
/// <typeparam name="TRequest">Тип запроса.</typeparam>
/// <typeparam name="TResponse">Тип ответа на запрос.</typeparam>
public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <inheritdoc />
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (next == null)
        {
            throw new ArgumentNullException(nameof(next));
        }

        var context = new ValidationContext<TRequest>(request);

        var errors = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToArray();

        if (errors.Any())
        {
            throw new ValidationException("Ошибка валидации", errors);
        }

        return next();
    }
}
