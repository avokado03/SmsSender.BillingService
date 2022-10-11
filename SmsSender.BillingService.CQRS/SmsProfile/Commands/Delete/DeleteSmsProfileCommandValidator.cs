using FluentValidation;

namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.Delete;

/// <summary>
/// Валидатор для <see cref="DeleteSmsProfileCommand"/>
/// </summary>
public class DeleteSmsProfileCommandValidator : AbstractValidator<DeleteSmsProfileCommand>
{
    public DeleteSmsProfileCommandValidator()
    {
        RuleFor(x => x.SmsProfileId)
            .GreaterThan(0)
            .WithMessage("Идентификатор смс-профиля должен быть больше нуля.");
    }
}
