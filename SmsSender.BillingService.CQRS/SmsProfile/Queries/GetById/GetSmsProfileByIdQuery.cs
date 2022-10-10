using MediatR;

namespace SmsSender.BillingService.CQRS.SmsProfile.Queries.GetById;

/// <summary>
/// Запрос на получение смс-профиля по его идентификатору
/// </summary>
public class GetSmsProfileByIdQuery : IRequest<GetSmsProfileByIdResponse>
{
    /// <summary>
    /// Идентификатор профиля
    /// </summary>
    public int SmsProfileId { get; set; }
}
