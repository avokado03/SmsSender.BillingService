using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmsSender.BillingService.Data;

namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.SendMessage;

/// <summary>
/// Обработчик команды <see cref="SendMessageCommand"/>
/// </summary>
public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, SendMessageResponse>
{
    private readonly BillingDbContext _dbContext;
    private readonly IMapper _mapper;

    public SendMessageCommandHandler(IMapper mapper, BillingDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
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

        response.IsSended = true;
        return response;
    }
}
