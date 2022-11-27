using MediatR;
using Microsoft.EntityFrameworkCore;
using SmsSender.BillingService.CQRS.SmsProfile.Messages;
using SmsSender.BillingService.Data;
using SmsSender.Common.RabbitMQ.Interfaces;
using SmsSender.Common.Redis;

namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.SendMessage;

/// <summary>
/// Обработчик команды <see cref="SendMessageCommand"/>
/// </summary>
public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, SendMessageResponse>
{
    private readonly BillingDbContext _dbContext;
    private IRabbitClient _rabbitClient;
    private IRedisRateLimiter _rateLimiter;


    public SendMessageCommandHandler(BillingDbContext dbContext, IRabbitClient rabbitClient,
        IRedisRateLimiter rateLimiter)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _rabbitClient = rabbitClient;
        _rateLimiter = rateLimiter;
    }

    /// <inheritdoc />
    public async Task<SendMessageResponse> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var response = new SendMessageResponse();
        response.IsSended = true;

        var profile = await _dbContext.SmsProfiles.SingleOrDefaultAsync(x => x.SmsProfileId == request.SmsProfileId)
            .ConfigureAwait(false);

        try
        {
            if (profile == null)
            {
                throw new Exception("Профиль не найден");
            }

            if (profile.IsBlocked)
            {
                throw new Exception("Профиль заблокирован");
            }

            // redis
            bool isAllowded = _rateLimiter.CheckLimit($"profile_{profile.SmsProfileId}",
                profile.MessagePerMinute,
                new TimeSpan(0, 1, 0));

            if (!isAllowded)
            {
                throw new Exception("Лимит сообщений в минуту превышен");
            }
        }
        catch(Exception ex)
        {
            response.IsSended = false;
            response.Errors = new List<string> { ex.Message };
            return response;
        }

        profile.MessageCount--;

        if (profile.MessageCount == 0)
        {
            profile.IsBlocked = true;
        }

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        // rabbit
        if (profile.IsBlocked)
        {
            _rabbitClient.Publish(new EmailNotificationMessage
            {
                Content = "Ваш лимит сообщений исчерпан. Профиль заблокирован",
                Email = profile.Email
            }, "email_notification");
        }
      
        return response;
    }
}
