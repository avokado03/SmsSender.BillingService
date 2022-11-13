namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.SendMessage
{
    /// <summary>
    /// Ответ для команды <see cref="SendMessageCommand"/>
    /// </summary>
    public class SendMessageResponse
    {
        /// <summary>
        /// Результат отправки
        /// </summary>
        public bool IsSended { get; set; }

        /// <summary>
        /// Список ошибок
        /// </summary>
        public string[] Errors { get; set; }
    }
}
