using MediatR;

namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.SendMessage
{
    /// <summary>
    /// Команда для отправки сообщения из профиля
    /// </summary>
    public class SendMessageCommand : IRequest<SendMessageResponse>
    {
        /// <summary>
        /// Идентификатор профиля
        /// </summary>
        public int SmsProfileId { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Message { get; set; }
    }
}
