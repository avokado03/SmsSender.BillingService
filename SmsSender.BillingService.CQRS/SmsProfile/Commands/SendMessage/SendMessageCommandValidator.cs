using FluentValidation;

namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.SendMessage;

/// <summary>
/// Валидатор для <see cref="SendMessageCommand"/>
/// </summary>
public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.SmsProfileId)
            .NotEmpty()
            .WithMessage("Идентификатор смс-профиля не может быть пустым")
            .NotEqual(Guid.Empty)
            .WithMessage("Значение идентификатора должно отличаться от значения по умодчанию");

        RuleFor(x => x.Message)
            .MinimumLength(4)
            .WithMessage("Длина сообщения от 4 до 40 символов")
            .MaximumLength(40)
            .WithMessage("Длина сообщения от 4 до 40 символов")
            .NotNull()
            .WithMessage("Сообщение должно быть указано")
            .NotEmpty()
            .WithMessage("Сообщение не может быть пустым");
    }
}
