using Moq;
using NUnit.Framework;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data;
using STAREvents.Web.ViewModels.Notifications;
using System.Linq.Expressions;
using STAREvents.Common;
using MockQueryable.Moq;
using static STAREvents.Common.ErrorMessagesConstants.NotificationsServiceErrorMessages;

namespace STAREvents.Services.Tests
{
    [TestFixture]
    public class NotificationsServiceTests
    {
        private Mock<IRepository<Event, object>> _eventRepositoryMock;
        private Mock<IRepository<Notification, object>> _notificationRepositoryMock;
        private NotificationsService _notificationsService;

        [SetUp]
        public void SetUp()
        {
            _eventRepositoryMock = new Mock<IRepository<Event, object>>();
            _notificationRepositoryMock = new Mock<IRepository<Notification, object>>();

            _notificationsService = new NotificationsService(
                _eventRepositoryMock.Object,
                _notificationRepositoryMock.Object
            );
        }

        [Test]
        public async Task SendEventNotificationsAsync_AddsNotifications_WhenNoExistingNotification()
        {
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var events = new List<Event>
                {
                    new Event
                    {
                        EventId = eventId,
                        Name = "Event 1",
                        StartDate = DateTime.UtcNow.AddDays(1),
                        UserEventAttendances = new List<UserEventAttendance>
                        {
                            new UserEventAttendance { UserId = userId }
                        }
                    }
                };

            var mockQueryableEvents = events.AsQueryable().BuildMockDbSet();

            _eventRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(mockQueryableEvents.Object);

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            _notificationRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Notification, bool>>>()))
                .ReturnsAsync((Notification?)null);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            await _notificationsService.SendEventNotificationsAsync();

            _notificationRepositoryMock.Verify(r => r.AddAsync(It.Is<Notification>(
                n => n.UserId == userId &&
                     n.EventId == eventId &&
                     n.Message.Contains("Event 1"))), Times.Once);
        }


        [Test]
        public async Task SendEventNotificationsAsync_DoesNotAddDuplicateNotifications()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var events = new List<Event>
                {
                    new Event
                    {
                        EventId = eventId,
                        Name = "Event 1",
                        StartDate = DateTime.UtcNow.AddDays(1),
                        UserEventAttendances = new List<UserEventAttendance>
                        {
                            new UserEventAttendance { UserId = userId }
                        }
                    }
                };

                        var notifications = new List<Notification>
                {
                    new Notification
                    {
                        UserId = userId,
                        EventId = eventId,
                        Message = "Reminder: The event 'Event 1' is happening tomorrow!"
                    }
                };

            var mockEventsDbSet = events.AsQueryable().BuildMockDbSet();
            _eventRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(mockEventsDbSet.Object);

            var mockNotificationsDbSet = notifications.AsQueryable().BuildMockDbSet();
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            _notificationRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Notification, bool>>>()))
                .ReturnsAsync((Expression<Func<Notification, bool>> predicate) =>
                    notifications.AsQueryable().FirstOrDefault(predicate));
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            await _notificationsService.SendEventNotificationsAsync();

            _notificationRepositoryMock.Verify(
                r => r.AddAsync(It.IsAny<Notification>()),
                Times.Never);
        }


        [Test]
        public async Task GetUserNotificationsAsync_ReturnsUnreadNotifications()
        {
            var userId = Guid.NewGuid();

            var notifications = new List<Notification>
            {
                new Notification { UserId = userId, IsRead = false, Message = "Unread Notification 1" },
                new Notification { UserId = userId, IsRead = true, Message = "Read Notification" },
                new Notification { UserId = Guid.NewGuid(), IsRead = false, Message = "Another User's Notification" }
            };

            _notificationRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(notifications);

            var result = await _notificationsService.GetUserNotificationsAsync(userId);

            Assert.That(result.Succeeded, Is.True);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.That(result.Data.Count, Is.EqualTo(1));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            Assert.That(result.Data[0].Message, Is.EqualTo("Unread Notification 1"));
        }

        [Test]
        public async Task MarkAsReadAsync_ReturnsSuccess_WhenNotificationExistsAndUnread()
        {
            var notificationId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var notification = new Notification
            {
                NotificationId = notificationId,
                UserId = userId,
                IsRead = false
            };

            _notificationRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Notification, bool>>>()))
                .ReturnsAsync(notification);

            var result = await _notificationsService.MarkAsReadAsync(notificationId, userId);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(notification.IsRead, Is.True);
            _notificationRepositoryMock.Verify(r => r.UpdateAsync(notification), Times.Once);
        }

        [Test]
        public async Task MarkAsReadAsync_ReturnsFailure_WhenNotificationNotFound()
        {
            var notificationId = Guid.NewGuid();
            var userId = Guid.NewGuid();

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            _notificationRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Notification, bool>>>()))
                .ReturnsAsync((Notification?)null);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            var result = await _notificationsService.MarkAsReadAsync(notificationId, userId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(NotificationNotFound));
        }

        [Test]
        public async Task MarkAsReadAsync_ReturnsFailure_WhenNotificationAlreadyRead()
        {
            var notificationId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var notification = new Notification
            {
                NotificationId = notificationId,
                UserId = userId,
                IsRead = true
            };

            _notificationRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Notification, bool>>>()))
                .ReturnsAsync(notification);

            var result = await _notificationsService.MarkAsReadAsync(notificationId, userId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(NotificationAlreadyMarked));
        }
    }
}
