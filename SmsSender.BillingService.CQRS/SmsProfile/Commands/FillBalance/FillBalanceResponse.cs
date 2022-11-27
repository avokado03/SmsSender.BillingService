namespace SmsSender.BillingService.CQRS.SmsProfile.Commands.FillBalance
{
    /// <summary>
    /// Ответ для команды <see cref="FillBalanceCommand"/>
    /// </summary>
    public class FillBalanceResponse
    {
        /// <summary>
        /// Признак успешного пополнения
        /// </summary>
        public bool Successed { get; set; }

        /// <summary>
        /// Список ошибок
        /// </summary>
        public List<string> Errors { get; set; }
    }
}
