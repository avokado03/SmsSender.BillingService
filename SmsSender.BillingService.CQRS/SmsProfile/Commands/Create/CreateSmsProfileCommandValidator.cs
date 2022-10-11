using FluentValidation;

namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.Create;

/// <summary>
/// Валидатор для <see cref="CreateSmsProfileCommand"/>
/// </summary>
public class CreateSmsProfileCommandValidator : AbstractValidator<CreateSmsProfileCommand>
{
    public CreateSmsProfileCommandValidator()
    {
        RuleFor(x => x.SmsProfileOnCreating)
            .NotNull()
            .WithMessage("Данные о добавляемом смс-профиле отсутствуют.");

        RuleFor(x => x.SmsProfileOnCreating.ClientId)            
            .NotNull()
            .WithMessage("Идентификатор клиента должен быть указан.")
            .NotEmpty()
            .WithMessage("Идентификатор клиента должен не должен быть пустым.");

        RuleFor(x => (int)x.SmsProfileOnCreating.MessageCount)
            .GreaterThan(0)
            .WithMessage("Первоначальный лимит сообщений должно быть больше нуля.");

        RuleFor(x => (int)x.SmsProfileOnCreating.MessagePerMinute)
            .GreaterThan(0)
            .WithMessage("Количество сообщениний в минуту должно быть больше нуля.");

        RuleFor(x => x.SmsProfileOnCreating)
           .Must(x => x.MessagePerMinute <= x.MessageCount)
           .WithMessage("Первоначально количество сообщений в минуту должно быть меньше или равно лимита сообщений.");
    }
}
