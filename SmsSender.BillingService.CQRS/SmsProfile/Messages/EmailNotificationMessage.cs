using SmsSender.Common.RabbitMQ.Interfaces;

namespace SmsSender.BillingService.CQRS.SmsProfile.Messages
{
    public class EmailNotificationMessage : IMessage
    {
        public string Email { get; set; }
        public string Content { get; set; }
    }
}
