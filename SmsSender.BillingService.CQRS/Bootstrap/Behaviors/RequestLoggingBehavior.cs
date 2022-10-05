using MediatR;
using Microsoft.Extensions.Logging;

namespace SmsSender.BillingService.CQRS.Bootstrap.Behaviors;

/// <summary>
/// Осуществляет логирование данных запроса.
/// </summary>
/// <typeparam name="TRequest">Тип запроса.</typeparam>
/// <typeparam name="TResponse">Тип ответа на запрос.</typeparam>
public class RequestLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;

    public RequestLoggingBehavior(ILoggerFactory loggerFactory)
    {
        if (loggerFactory == null)
        {
            throw new ArgumentNullException(nameof(loggerFactory));
        }
        _logger = loggerFactory.CreateLogger("SmsSender.BillingService.RequestLoggingBehavior");
    }

    /// <inheritdoc />
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (next == null)
        {
            throw new ArgumentNullException(nameof(next));
        }
        try
        {
            var name = typeof(TRequest).Name;
            _logger.LogInformation("Запрос: {name} {@request}", name, request);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Ошибка логирования данных запроса.");
        }

        return next();
    }
}
