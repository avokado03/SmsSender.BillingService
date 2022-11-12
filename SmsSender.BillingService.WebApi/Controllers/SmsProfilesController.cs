using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmsSender.BillingService.CQRS.SmsProfile.Commands.Create;
using SmsSender.BillingService.CQRS.SmsProfile.Commands.Delete;
using SmsSender.BillingService.CQRS.SmsProfile.Queries.Get;
using SmsSender.BillingService.CQRS.SmsProfile.Queries.GetById;

namespace SmsSender.BillingService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SmsProfilesController : ControllerBase
{
    private readonly ILogger<SmsProfilesController> _logger;
    private readonly IMediator _mediator;

    public SmsProfilesController(ILogger<SmsProfilesController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetSmsProfilesResponse))]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetSmsProfilesQuery(), cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetSmsProfileByIdResponse))]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetSmsProfileByIdQuery { SmsProfileId = id }, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateSmsProfileResponse))]
    public async Task<IActionResult> Post(CreateSmsProfileCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(DeleteSmsProfileCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(response);
    }
}