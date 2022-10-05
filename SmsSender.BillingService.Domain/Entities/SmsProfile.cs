namespace SmsSender.BillingService.Domain.Entities;

public partial class SmsProfile
{
    public int SmsProfileId { get; set; }
    public Guid ClientId { get; set; }
    public short MessageCount { get; set; }
    public short MessagePerMinute { get; set; }
    public bool IsBlocked { get; set; }
}
