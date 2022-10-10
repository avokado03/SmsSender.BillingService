using SmsSender.BillingService.CQRS.SmsProfile.Dto;

namespace SmsSender.BillingService.CQRS.SmsProfile.Queries.GetById;

/// <summary>
/// Ответ на запрос <see cref="GetSmsProfileByIdQuery"/>
/// </summary>
public class GetSmsProfileByIdResponse
{
    /// <summary>
    /// Профиль с Id из запроса
    /// </summary>
    public SmsProfileDto SmsProfile{ get; set; }
}
