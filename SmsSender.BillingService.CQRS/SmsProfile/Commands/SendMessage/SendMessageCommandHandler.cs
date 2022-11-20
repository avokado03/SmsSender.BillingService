using MediatR;
using Microsoft.EntityFrameworkCore;
using SmsSender.BillingService.CQRS.SmsProfile.Messages;
using SmsSender.BillingService.Data;
using SmsSender.Common.RabbitMQ.Interfaces;

namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.SendMessage;

/// <summary>
/// Обработчик команды <see cref="SendMessageCommand"/>
/// </summary>
public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, SendMessageResponse>
{
    private readonly BillingDbContext _dbContext;
    private IRabbitClient _rabbitClient;

    public SendMessageCommandHandler(BillingDbContext dbContext, IRabbitClient rabbitClient)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _rabbitClient = rabbitClient;
    }

    /// <inheritdoc />
    public async Task<SendMessageResponse> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var response = new SendMessageResponse();

        var profile = await _dbContext.SmsProfiles.SingleOrDefaultAsync(x => x.SmsProfileId == request.SmsProfileId).ConfigureAwait(false);

        if (profile == null)
        {
            response.IsSended = false;
            response.Errors = new[] { "Профиль не найден" };
            return response;
        }

        if (profile.IsBlocked)
        {
            response.IsSended = false;
            response.Errors = new[] { "Профиль заблокирован" };
            return response;
        }

        // redis

        profile.MessageCount--;

        if (profile.MessageCount == 0)
        {
            profile.IsBlocked = true;
        }

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        // rabbit
        _rabbitClient.Publish(new EmailNotificationMessage {
            Content = "Ваш лимит сообщений исчерпан. Профиль заблокирован", 
            Email = profile.Email
        }, "email_notification");

        response.IsSended = true;
        return response;
    }
}
