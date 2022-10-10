using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SmsSender.BillingService.Domain;

public static class DbContextExtensions
{
    /// <summary>
    /// Добавление в DI-контейнер экземпляра
    /// контекста БД
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <param name="configuration">Конфигурация приложения</param>
    public static void AddDbContext<TContext>(this IServiceCollection services, IConfiguration configuration)
        where TContext : DbContext =>

    services.AddDbContext<TContext>(options =>
       options.UseNpgsql(
                configuration.GetConnectionString(typeof(TContext).Name)
        ).EnableSensitiveDataLogging(true),
    contextLifetime: ServiceLifetime.Scoped,
    optionsLifetime: ServiceLifetime.Singleton);
}
