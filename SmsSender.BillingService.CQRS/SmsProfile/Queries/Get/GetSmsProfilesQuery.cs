using MediatR;

namespace SmsSender.BillingService.CQRS.SmsProfile.Queries.Get;

/// <summary>
/// Запрос на получение всех смс-профилей
/// </summary>
public class GetSmsProfilesQuery : IRequest<GetSmsProfilesResponse>
{
}
