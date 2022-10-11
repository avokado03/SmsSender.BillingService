namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.Create;

/// <summary>
/// Ответ на запрос <see cref="CreateSmsProfileCommand"/>
/// </summary>
public class CreateSmsProfileResponse
{
    /// <summary>
    /// Идентификатор нового смс - профиля
    /// </summary>
    public int SmsProfileId { get; set; }
}
