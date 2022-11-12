using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmsSender.BillingService.CQRS.SmsProfile.Dto;
using SmsSender.BillingService.Data;

namespace SmsSender.BillingService.CQRS.SmsProfile.Queries.GetById;

/// <summary>
/// Обработчик запроса <see cref="GetSmsProfileByIdQuery"/>
/// </summary>
public class GetSmsProfileByIdQueryHandler : IRequestHandler<GetSmsProfileByIdQuery, GetSmsProfileByIdResponse>
{
    private readonly BillingDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSmsProfileByIdQueryHandler(IMapper mapper, BillingDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<GetSmsProfileByIdResponse> Handle(GetSmsProfileByIdQuery request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var result = await _dbContext.SmsProfiles
            .SingleOrDefaultAsync(x => x.SmsProfileId == request.SmsProfileId)
            .ConfigureAwait(false);

        if (result == null)
        {
            throw new NullReferenceException("Смс-профиль не существует или уже удален.");
        }

        var response = new GetSmsProfileByIdResponse 
        { 
            SmsProfile = _mapper.Map<SmsProfileDto>(result) 
        };

        return response;
    }
}
