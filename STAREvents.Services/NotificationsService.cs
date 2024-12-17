using Microsoft.EntityFrameworkCore;
using STAREvents.Common;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Notifications;
using static STAREvents.Common.ErrorMessagesConstants.NotificationsServiceErrorMessages;
using static STAREvents.Common.MessagesConstants.Notifcations;

namespace STAREvents.Services.Data
{
    public class NotificationsService : BaseService, INotificationsService
    {
        private readonly IRepository<Event, object> _eventRepository;
        private readonly IRepository<Notification, object> _notificationRepository;

        public NotificationsService(
            IRepository<Event, object> eventRepository,
            IRepository<Notification, object> notificationRepository)
        {
            _eventRepository = eventRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task SendEventNotificationsAsync()
        {
            var upcomingEvents = await _eventRepository.GetAllAttached()
                .Where(e => e.StartDate.Date == DateTime.UtcNow.AddDays(1).Date)
                .Include(e => e.UserEventAttendances)
                .ToListAsync();

            foreach (var ev in upcomingEvents)
            {
                foreach (var attendee in ev.UserEventAttendances)
                {
                    var existingNotification = await _notificationRepository
                        .FirstOrDefaultAsync(n => n.UserId == attendee.UserId && n.EventId == ev.EventId);

                    if (existingNotification == null) 
                    {
                        var notification = new Notification
                        {
                            UserId = attendee.UserId,
                            EventId = ev.EventId,
                            Message = string.Format(EventReminderMessage, ev.Name)
                        };

                        await _notificationRepository.AddAsync(notification);
                    }
                }
            }
        }

        public async Task<ServiceResult<List<NotificationViewModel>>> GetUserNotificationsAsync(Guid userId)
        {
            var notifications = await _notificationRepository.GetAllAsync();
            var userNotifications = notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .Select(n => new NotificationViewModel
                {
                    Message = n.Message,
                    CreatedOn = n.CreatedOn,
                    IsRead = n.IsRead,
                    EventId = n.EventId,
                    NotificationId = n.NotificationId
                })
                .ToList();

            return ServiceResult<List<NotificationViewModel>>.Success(userNotifications);
        }

        public async Task<ServiceResult> MarkAsReadAsync(Guid notificationId, Guid userId)
        {
            var notification = await _notificationRepository
                .FirstOrDefaultAsync(n => n.NotificationId == notificationId && n.UserId == userId);

            if (notification == null)
            {
                return ServiceResult.Failure(NotificationNotFound);
            }

            if (notification.IsRead)
            {
                return ServiceResult.Failure(NotificationAlreadyMarked);
            }

            notification.IsRead = true;
            await _notificationRepository.UpdateAsync(notification);

            return ServiceResult.Success();
        }


    }
}
