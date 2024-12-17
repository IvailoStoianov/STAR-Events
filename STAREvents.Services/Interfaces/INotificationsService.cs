using STAREvents.Common;
using STAREvents.Web.ViewModels.Notifications;

namespace STAREvents.Services.Data.Interfaces
{
    public interface INotificationsService
    {
        Task SendEventNotificationsAsync();
        Task<ServiceResult<List<NotificationViewModel>>> GetUserNotificationsAsync(Guid userId);
        Task<ServiceResult> MarkAsReadAsync(Guid notificationId, Guid userId);

    }
}
