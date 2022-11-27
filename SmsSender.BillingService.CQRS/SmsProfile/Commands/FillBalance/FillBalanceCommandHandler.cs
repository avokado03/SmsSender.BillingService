using MediatR;
using Microsoft.EntityFrameworkCore;
using SmsSender.BillingService.CQRS.SmsProfile.Messages;
using SmsSender.BillingService.Data;
using SmsSender.Common.RabbitMQ.Interfaces;

namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.FillBalance;

/// <summary>
/// Хэндлер для обработки команды <see cref="FillBalanceCommand"/>
/// </summary>
public class FillBalanceCommandHandler : IRequestHandler<FillBalanceCommand, FillBalanceResponse>
{
    private readonly BillingDbContext _dbContext;
    private IRabbitClient _rabbitClient;

    public FillBalanceCommandHandler(BillingDbContext dbContext, IRabbitClient rabbitClient)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _rabbitClient = rabbitClient;
    }

    /// <inheritdoc />
    public async Task<FillBalanceResponse> Handle(FillBalanceCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var response = new FillBalanceResponse();
        response.Successed = true;

        var profile = await _dbContext.SmsProfiles.SingleOrDefaultAsync(x => x.SmsProfileId == request.SmsProfileId)
            .ConfigureAwait(false);

        try
        {
            if (profile == null)
            {
                throw new Exception("Профиль не найден");
            }

            profile.MessageCount += request.MessageCount;

            bool unblocked = false;
            if(profile.IsBlocked && profile.MessageCount > 0)
            {
                profile.IsBlocked = false;
                unblocked = true;
            }

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            _rabbitClient.Publish(new EmailNotificationMessage
            {
                Content = $"Ваш баланс пополнен на {request.MessageCount} сообщений",
                Email = profile.Email
            }, "email_notification");

            if (unblocked)
            {
                _rabbitClient.Publish(new EmailNotificationMessage
                {
                    Content = "Ваш профиль разблокирован",
                    Email = profile.Email
                }, "email_notification");
            }
        }
        catch (Exception ex)
        {
            response.Successed = false;
            response.Errors = new List<string> { ex.Message };
            return response;
        }

        return response;
    }
}
