namespace STAREvents.Web.ViewModels.Notifications
{
    public class NotificationViewModel
    {
        public string Message { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public bool IsRead { get; set; }
        public Guid EventId { get; set; }
        public Guid NotificationId { get; set; }
    }
}
