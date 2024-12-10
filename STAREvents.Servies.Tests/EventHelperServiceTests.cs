using System.Linq.Expressions;

using Moq;

using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data;
using STAREvents.Services.Data.Interfaces;

using static STAREvents.Common.ErrorMessagesConstants.EventsServiceErrorMessages;

namespace STAREvents.Services.Tests
{
    [TestFixture]
    public class EventHelperServiceTests
    {
        private Mock<IRepository<Event, object>> eventRepositoryMock;
        private Mock<IRepository<UserEventAttendance, object>> attendanceRepositoryMock;
        private Mock<IUserAuthService> userAuthServiceMock;
        private EventHelperService eventHelperService;

        [SetUp]
        public void SetUp()
        {
            eventRepositoryMock = new Mock<IRepository<Event, object>>();
            attendanceRepositoryMock = new Mock<IRepository<UserEventAttendance, object>>();
            userAuthServiceMock = new Mock<IUserAuthService>();

            eventHelperService = new EventHelperService(
                eventRepositoryMock.Object,
                attendanceRepositoryMock.Object,
                userAuthServiceMock.Object
            );
        }

        [Test]
        public async Task JoinEventAsync_UserNotFound_ThrowsKeyNotFoundException()
        {
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync((ApplicationUser)null);

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await eventHelperService.JoinEventAsync(eventId, userId)
            );

            Assert.That(ex.Message, Is.EqualTo(UserNotFound));
        }

        [Test]
        public async Task JoinEventAsync_EventNotFound_ThrowsKeyNotFoundException()
        {
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId };

            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(user);
            eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId))
                .ReturnsAsync((Event)null);

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await eventHelperService.JoinEventAsync(eventId, userId)
            );

            Assert.That(ex.Message, Is.EqualTo(EventNotFound));
        }

        [Test]
        public async Task JoinEventAsync_AlreadyJoined_DoesNotAddAttendance()
        {
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId };
            var eventEntity = new Event { EventId = eventId, NumberOfParticipants = 10 };

            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(user);
            eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId))
                .ReturnsAsync(eventEntity);
            attendanceRepositoryMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<UserEventAttendance, bool>>>()))
                .ReturnsAsync(new UserEventAttendance { EventId = eventId, UserId = userId });

            await eventHelperService.JoinEventAsync(eventId, userId);

            attendanceRepositoryMock.Verify(x => x.AddAsync(It.IsAny<UserEventAttendance>()), Times.Never);
            eventRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Event>()), Times.Never);
        }

        [Test]
        public async Task JoinEventAsync_AddsAttendanceSuccessfully()
        {
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId };
            var eventEntity = new Event { EventId = eventId, NumberOfParticipants = 10 };

            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(user);
            eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId))
                .ReturnsAsync(eventEntity);
            attendanceRepositoryMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<UserEventAttendance, bool>>>()))
                .ReturnsAsync((UserEventAttendance)null);

            await eventHelperService.JoinEventAsync(eventId, userId);

            Assert.That(eventEntity.NumberOfParticipants, Is.EqualTo(11));
            attendanceRepositoryMock.Verify(x => x.AddAsync(It.Is<UserEventAttendance>(a =>
                a.EventId == eventId &&
                a.UserId == userId &&
                a.JoinedDate <= DateTime.UtcNow)), Times.Once);
            eventRepositoryMock.Verify(x => x.UpdateAsync(eventEntity), Times.Once);
        }

        [Test]
        public async Task LeaveEventAsync_UserNotFound_ThrowsKeyNotFoundException()
        {
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync((ApplicationUser)null);

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await eventHelperService.LeaveEventAsync(eventId, userId)
            );

            Assert.That(ex.Message, Is.EqualTo(UserNotFound));
        }

        [Test]
        public async Task LeaveEventAsync_EventNotFound_ThrowsKeyNotFoundException()
        {
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId };

            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(user);
            eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId))
                .ReturnsAsync((Event)null);

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await eventHelperService.LeaveEventAsync(eventId, userId)
            );

            Assert.That(ex.Message, Is.EqualTo(EventNotFound));
        }

        [Test]
        public async Task LeaveEventAsync_NotJoined_DoesNotRemoveAttendance()
        {
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId };
            var eventEntity = new Event { EventId = eventId, NumberOfParticipants = 10 };

            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(user);
            eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId))
                .ReturnsAsync(eventEntity);
            attendanceRepositoryMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<UserEventAttendance, bool>>>()))
                .ReturnsAsync((UserEventAttendance)null);

            await eventHelperService.LeaveEventAsync(eventId, userId);

            attendanceRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<UserEventAttendance>()), Times.Never);
            eventRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Event>()), Times.Never);
        }

        [Test]
        public async Task LeaveEventAsync_RemovesAttendanceSuccessfully()
        {
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId };
            var eventEntity = new Event { EventId = eventId, NumberOfParticipants = 10 };
            var attendance = new UserEventAttendance { EventId = eventId, UserId = userId };

            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(user);
            eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId))
                .ReturnsAsync(eventEntity);
            attendanceRepositoryMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<UserEventAttendance, bool>>>()))
                .ReturnsAsync(attendance);

            await eventHelperService.LeaveEventAsync(eventId, userId);

            Assert.That(eventEntity.NumberOfParticipants, Is.EqualTo(9));
            attendanceRepositoryMock.Verify(x => x.DeleteAsync(attendance), Times.Once);
            eventRepositoryMock.Verify(x => x.UpdateAsync(eventEntity), Times.Once);
        }
    }
}
