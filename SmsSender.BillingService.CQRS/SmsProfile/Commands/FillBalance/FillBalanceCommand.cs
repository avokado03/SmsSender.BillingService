using MediatR;

namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.FillBalance;

/// <summary>
/// Команда для пополнения количества сообщений
/// на балансе профиля
/// </summary>
public class FillBalanceCommand : IRequest<FillBalanceResponse>
{
    /// <summary>
    /// Идентификатор профиля
    /// </summary>
    public int SmsProfileId { get; set; }

    /// <summary>
    /// Количество сообщений
    /// </summary>
    public int MessageCount { get; set; }
}
