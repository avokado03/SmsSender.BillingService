using MediatR;

namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.Delete;

/// <summary>
/// Команда для удаления смс-профиля
/// </summary>
public class DeleteSmsProfileCommand : IRequest
{
    /// <summary>
    /// Идентификатор удаляемого профиля
    /// </summary>
    public int SmsProfileId { get; set; }
}
