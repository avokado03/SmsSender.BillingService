using MediatR;
using Microsoft.EntityFrameworkCore;
using SmsSender.BillingService.Data;

namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.Delete;

/// <summary>
/// Обработчик команды <see cref="DeleteSmsProfileCommand"/>
/// </summary>
public class DeleteSmsProfileCommandHandler : IRequestHandler<DeleteSmsProfileCommand, Unit>
{
    private readonly BillingDbContext _dbContext;

    public DeleteSmsProfileCommandHandler(BillingDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(DeleteSmsProfileCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var result = await _dbContext.SmsProfiles
            .SingleOrDefaultAsync(x => x.SmsProfileId == request.SmsProfileId).ConfigureAwait(false);

        if (result == null)
        {
            throw new NullReferenceException("Смс-профиль не существует или уже удален.");
        }

        _dbContext.Entry(result).State = EntityState.Deleted;

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        return Unit.Value;
    }
}
