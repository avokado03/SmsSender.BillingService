using SmsSender.BillingService.CQRS.SmsProfile.Dto;

namespace SmsSender.BillingService.CQRS.SmsProfile.Queries.Get;

/// <summary>
/// Ответ на запрос <see cref="GetSmsProfilesQuery"/>
/// </summary>
public class GetSmsProfilesResponse
{
    /// <summary>
    /// Список смс-профилей
    /// </summary>
    public IEnumerable<SmsProfileDto> SmsProfiles { get; set; }

    /// <summary>
    /// Количество профилей
    /// </summary>
    public int Count { get; set; }
}
