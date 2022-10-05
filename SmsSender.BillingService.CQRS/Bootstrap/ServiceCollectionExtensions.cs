using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmsSender.BillingService.Domain;

namespace SmsSender.BillingService.CQRS.Bootstrap;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление в DI-контейнер экземпляра
    /// сервмсов, используемых для обработки команд и запросов
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <param name="configuration">Конфигурация приложения</param>
    public static IServiceCollection UseCQRS(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BillingDbContext>(configuration);
        return services;
    }
}
