using FluentValidation;

namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.FillBalance;

/// <summary>
/// Валидатор для <see cref="FillBalanceCommand"/>
/// </summary>
public class FillBalanceCommandValidator : AbstractValidator<FillBalanceCommand>
{
    public FillBalanceCommandValidator()
    {
        RuleFor(x => x.SmsProfileId)
            .NotNull()
            .WithMessage("Идентификатор смс-профиля должен быть указан")
            .GreaterThan(0)
            .WithMessage("Идентификатор смс-профиля должен быть больше нуля");

        RuleFor(x => x.MessageCount)
            .GreaterThan(0)
            .WithMessage("Количество сообщений должно быть больше нуля");
    }
}
