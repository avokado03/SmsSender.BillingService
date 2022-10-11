using AutoMapper;
using MediatR;
using SmsSender.BillingService.Domain;

namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.Create;

/// <summary>
/// Обработчик команды <see cref="CreateSmsProfileCommand"/>
/// </summary>
public class CreateSmsProfileCommandHandler : IRequestHandler<CreateSmsProfileCommand, CreateSmsProfileResponse>
{
    private readonly BillingDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateSmsProfileCommandHandler(IMapper mapper, BillingDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<CreateSmsProfileResponse> Handle(CreateSmsProfileCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var domainProfile = _mapper.Map<Domain.Entities.SmsProfile>(request.SmsProfileOnCreating);
        await _dbContext.AddAsync(domainProfile).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        return new CreateSmsProfileResponse { SmsProfileId = domainProfile.SmsProfileId };
    }
}
