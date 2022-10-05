﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmsSender.BillingService.CQRS.Bootstrap.Behaviors;
using SmsSender.BillingService.Domain;

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
        services.ConfigurePipeline();

        return services;
    }

    private static void ConfigurePipeline(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
    }
}