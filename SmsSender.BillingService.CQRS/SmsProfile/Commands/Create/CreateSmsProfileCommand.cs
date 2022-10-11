using MediatR;
using SmsSender.BillingService.CQRS.SmsProfile.Dto;

namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.Create;

/// <summary>
/// Команда добавления смс-профиля
/// </summary>
public class CreateSmsProfileCommand : IRequest<CreateSmsProfileResponse>
{
    /// <summary>
    /// Создаваемый смс-профиль
    /// </summary>
    public SmsProfileOnCreatingDto SmsProfileOnCreating { get; set; }
}
