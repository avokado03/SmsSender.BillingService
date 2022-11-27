namespace SmsSender.BillingService.Data.Entities;

/// <summary>
/// Смс-профиль
/// </summary>
public partial class SmsProfile
{
    /// <summary>
    /// Идентификатор профиля
    /// </summary>
    public int SmsProfileId { get; set; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Почта, привязанная к профилю
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Кол-во сообщений
    /// </summary>
    public int MessageCount { get; set; }

    /// <summary>
    /// Доступное кол-во сообщений с минуту
    /// </summary>
    public short MessagePerMinute { get; set; }

    /// <summary>
    /// Признак блокировки профиля
    /// </summary>
    public bool IsBlocked { get; set; }
}
