using FluentValidation;

namespace SmsSender.BillingService.CQRS.SmsProfile.Queries.GetById;

/// <summary>
/// Валидатор для <see cref="GetSmsProfileByIdQuery"/>
/// </summary>
public class GetSmsProfileByIdQueryValidator : AbstractValidator<GetSmsProfileByIdQuery>
{
    public GetSmsProfileByIdQueryValidator()
    {
        RuleFor(x => x.SmsProfileId)
            .GreaterThan(0)
            .WithMessage("Идентификатор смс-профиля должен быть больше нуля.");
    }
}
