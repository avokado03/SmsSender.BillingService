using AutoMapper;

namespace SmsSender.BillingService.CQRS.SmsProfile.Dto;

[AutoMap(typeof(Domain.Entities.SmsProfile))]
public class SmsProfileDto
{
    public int SmsProfileId { get; set; }
    public Guid ClientId { get; set; }
    public short MessageCount { get; set; }
    public short MessagePerMinute { get; set; }
    public bool IsBlocked { get; set; }
}
