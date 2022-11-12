using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmsSender.BillingService.CQRS.SmsProfile.Dto;
using SmsSender.BillingService.Data;

namespace SmsSender.BillingService.CQRS.SmsProfile.Queries.Get;

/// <summary>
/// Обработчик запроса <see cref="GetSmsProfilesQuery"/>
/// </summary>
public class GetSmsProfilesQueryHandler : IRequestHandler<GetSmsProfilesQuery, GetSmsProfilesResponse>
{
    private readonly BillingDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSmsProfilesQueryHandler(IMapper mapper, BillingDbContext dbContext)
    {        
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<GetSmsProfilesResponse> Handle(GetSmsProfilesQuery request, CancellationToken cancellationToken)
    {
        if(request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var result = await _dbContext.SmsProfiles.ToListAsync().ConfigureAwait(false);

        var response = new GetSmsProfilesResponse
        {
            SmsProfiles = _mapper.Map<List<SmsProfileDto>>(result),
            Count = result.Count
        };

        return response;
    }
}
