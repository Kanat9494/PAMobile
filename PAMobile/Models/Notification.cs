namespace PAMobile.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string? Inn { get; set; }

    public DateTime? SentDate { get; set; }

    public string? Text { get; set; }
}
