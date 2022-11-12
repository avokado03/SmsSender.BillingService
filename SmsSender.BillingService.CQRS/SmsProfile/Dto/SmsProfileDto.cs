using AutoMapper;

namespace SmsSender.BillingService.CQRS.SmsProfile.Dto;

/// <summary>
/// DTO для <see cref="Domain.Entities.SmsProfile"/>
/// </summary>
[AutoMap(typeof(Data.Entities.SmsProfile), ReverseMap = true)]
public class SmsProfileDto
{
    /// <summary>
    /// Идентификатор смс-профиля
    /// </summary>
    public int SmsProfileId { get; set; }

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

    /// <summary>
    /// Признак блокировки профиля
    /// </summary>
    public bool IsBlocked { get; set; }
}
