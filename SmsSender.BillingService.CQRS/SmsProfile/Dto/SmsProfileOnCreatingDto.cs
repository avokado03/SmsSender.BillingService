using AutoMapper;

namespace SmsSender.BillingService.CQRS.SmsProfile.Dto;

/// <summary>
/// DTO для создаваемого смс-профиля
/// </summary>
[AutoMap(typeof(Domain.Entities.SmsProfile), ReverseMap = true)]
public class SmsProfileOnCreatingDto
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Лимит сообщений
    /// </summary>
    public short MessageCount { get; set; }

    /// <summary>
    /// Сообщений в минуту
    /// </summary>
    public short MessagePerMinute { get; set; }
}
