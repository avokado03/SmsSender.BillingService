using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmsSender.Common.RabbitMQ;
using SmsSender.Common.Redis;
using SmsSender.BillingService.CQRS.Bootstrap.Behaviors;
using SmsSender.BillingService.CQRS.SmsProfile.Queries.Get;
using SmsSender.BillingService.CQRS.SmsProfile.Queries.GetById;
using SmsSender.BillingService.CQRS.SmsProfile.Commands.Create;
using SmsSender.BillingService.CQRS.SmsProfile.Commands.Delete;
using SmsSender.BillingService.Data;
using SmsSender.BillingService.CQRS.SmsProfile.Commands.SendMessage;
using SmsSender.BillingService.CQRS.SmsProfile.Commands.FillBalance;

namespace SmsSender.BillingService.CQRS.Bootstrap;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление в DI-контейнер экземпляра
    /// сервисов, используемых для обработки команд и запросов
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <param name="configuration">Конфигурация приложения</param>
    public static IServiceCollection UseCQRS(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BillingDbContext>(configuration);
        services.ConfigureRabbit(configuration);
        services.ConfigureRedis(configuration);
        services.ConfigurePipeline();
        services.ConfigureHandlers();
        return services;
    }

    private static void ConfigureHandlers(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<GetSmsProfilesQuery, GetSmsProfilesResponse>, GetSmsProfilesQueryHandler>();
        services.AddScoped<IRequestHandler<GetSmsProfileByIdQuery, GetSmsProfileByIdResponse>, GetSmsProfileByIdQueryHandler>();
        services.AddScoped<IRequestHandler<CreateSmsProfileCommand, CreateSmsProfileResponse>, CreateSmsProfileCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteSmsProfileCommand, Unit>, DeleteSmsProfileCommandHandler>();
        services.AddScoped<IRequestHandler<SendMessageCommand, SendMessageResponse>, SendMessageCommandHandler>();
        services.AddScoped<IRequestHandler<FillBalanceCommand, FillBalanceResponse>, FillBalanceCommandHandler>();
    }

    private static void ConfigurePipeline(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
    }
}
