using SmsSender.Common.RabbitMQ.Interfaces;

namespace SmsSender.BillingService.CQRS.SmsProfile.Messages;

/// <summary>
/// E-mail опоыещение
/// </summary>
public class EmailNotificationMessage : IMessage
{
    /// <summary>
    /// Адрес получателя
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Содержимое
    /// </summary>
    public string Content { get; set; }
}
