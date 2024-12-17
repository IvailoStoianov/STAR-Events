using MockQueryable;
using Moq;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.CreateEvents;
using Microsoft.Extensions.Configuration;
using static STAREvents.Common.ErrorMessagesConstants.EventsServiceErrorMessages;
using static STAREvents.Common.ModelErrorsConstants.Date;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using STAREvents.Web.ViewModels.Events;
using static STAREvents.Common.ErrorMessagesConstants;
using static STAREvents.Common.FilePathConstants;
using System.Text;

namespace STAREvents.Services.Tests
{
    [TestFixture]
    public class EventsServiceTests
    {
        private Mock<IRepository<Event, object>> _eventRepositoryMock;
        private Mock<IRepository<Category, object>> _categoryRepositoryMock;
        private Mock<IRepository<Comment, object>> _commentRepositoryMock;
        private Mock<IRepository<UserEventAttendance, object>> _attendanceRepositoryMock;
        private Mock<IUserAuthService> _userAuthServiceMock;
        private Mock<IFileStorageService> _fileStorageServiceMock;
        private Mock<IConfiguration> _configurationMock;
        private EventsService _eventsService;

        [SetUp]
        public void SetUp()
        {
            _eventRepositoryMock = new Mock<IRepository<Event, object>>();
            _categoryRepositoryMock = new Mock<IRepository<Category, object>>();
            _commentRepositoryMock = new Mock<IRepository<Comment, object>>();
            _attendanceRepositoryMock = new Mock<IRepository<UserEventAttendance, object>>();
            _userAuthServiceMock = new Mock<IUserAuthService>();
            _fileStorageServiceMock = new Mock<IFileStorageService>();
            _configurationMock = new Mock<IConfiguration>();

            var sectionMock = new Mock<IConfigurationSection>();

            sectionMock.Setup(s => s.Value).Returns("false");
            _configurationMock.Setup(c => c.GetSection("UseAzureBlobStorage")).Returns(sectionMock.Object);

            _eventsService = new EventsService(
                _eventRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                _commentRepositoryMock.Object,
                _attendanceRepositoryMock.Object,
                _userAuthServiceMock.Object,
                _fileStorageServiceMock.Object,
                _configurationMock.Object
            );
        }

        [Test]
        public async Task LoadEventsAsync_ReturnsSuccessResult_WithCorrectEventCount()
        {
            var events = new List<Event>
            {
                new Event { EventId = Guid.NewGuid(), Name = "Event 1", isDeleted = false },
                new Event { EventId = Guid.NewGuid(), Name = "Event 2", isDeleted = false }
            };

            _eventRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(events);
            _categoryRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Category>());

            var result = await _eventsService.LoadEventsAsync("", null, "Recent", 1, 10);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Data.Events.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetEventDetailsAsync_ReturnsFailure_WhenEventNotFound()
        {
            _eventRepositoryMock.Setup(x => x.GetAllAttached()).Returns(new List<Event>().AsQueryable().BuildMock());

            var result = await _eventsService.GetEventDetailsAsync(Guid.NewGuid());

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(EventNotFound));
        }

        [Test]
        public async Task CreateEventAsync_ReturnsFailure_WhenStartDateIsInvalid()
        {
            var model = new CreateEventInputModel
            {
                StartDate = DateTime.UtcNow.AddDays(2),
                EndDate = DateTime.UtcNow.AddDays(1)
            };

            var result = await _eventsService.CreateEventAsync(model, Guid.NewGuid());

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(StartDateBeforeEndDate));
        }

        [Test]
        public async Task CreateEventAsync_ReturnsSuccess_WhenValidModelProvided()
        {
            var model = new CreateEventInputModel
            {
                Name = "Test Event",
                Description = "Description",
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Capacity = 100,
                CategoryId = Guid.NewGuid(),
                Address = "Test Address"
            };

            _eventRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Event>())).Returns(Task.CompletedTask);

            var result = await _eventsService.CreateEventAsync(model, Guid.NewGuid());

            Assert.That(result.Succeeded, Is.True);
            _eventRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Event>()), Times.Once);
        }

        [Test]
        public async Task JoinEventAsync_ReturnsFailure_WhenUserNotFound()
        {
            var userId = Guid.NewGuid();
            _userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString())).ReturnsAsync((ApplicationUser)null);

            var result = await _eventsService.JoinEventAsync(Guid.NewGuid(), userId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(UserNotFound));
        }

        [Test]
        public async Task JoinEventAsync_IncreasesParticipantsCount_WhenSuccessful()
        {
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var eventEntity = new Event { EventId = eventId, NumberOfParticipants = 0, Capacity = 2 };

            _userAuthServiceMock
                .Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(new ApplicationUser { Id = userId });

            _eventRepositoryMock
                .Setup(x => x.GetByIdAsync(eventId))
                .ReturnsAsync(eventEntity);

            _attendanceRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<UserEventAttendance, bool>>>()))
                .ReturnsAsync((UserEventAttendance)null);

            var result = await _eventsService.JoinEventAsync(eventId, userId);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(eventEntity.NumberOfParticipants, Is.EqualTo(1));
            _eventRepositoryMock.Verify(x => x.UpdateAsync(eventEntity), Times.Once);
            _attendanceRepositoryMock.Verify(x => x.AddAsync(It.IsAny<UserEventAttendance>()), Times.Once);
        }

        [Test]
        public async Task AddCommentAsync_ReturnsFailure_WhenUserNotFound()
        {
            _userAuthServiceMock.Setup(x => x.GetUserByNameAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            var result = await _eventsService.AddCommentAsync(Guid.NewGuid(), "TestUser", "Test Comment");

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(UserNotFound));
        }

        [Test]
        public async Task SoftDeleteEventAsync_ReturnsFailure_WhenEventNotFound()
        {
            _eventRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Event)null);

            var result = await _eventsService.SoftDeleteEventAsync(Guid.NewGuid());

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(EventNotFound));
        }

        [Test]
        public async Task SoftDeleteEventAsync_SoftDeletesEvent_WhenSuccessful()
        {
            var eventId = Guid.NewGuid();
            var eventEntity = new Event { EventId = eventId, isDeleted = false };

            _eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId)).ReturnsAsync(eventEntity);

            var result = await _eventsService.SoftDeleteEventAsync(eventId);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(eventEntity.isDeleted, Is.True);
            _eventRepositoryMock.Verify(x => x.UpdateAsync(eventEntity), Times.Once);
        }

        [Test]
        public async Task LoadEventsAsync_ReturnsEmpty_WhenNoEventsExist()
        {
            _eventRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Event>());
            _categoryRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Category>());

            var result = await _eventsService.LoadEventsAsync("", null, "Recent", 1, 10);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Data.Events.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task CreateEventAsync_ReturnsFailure_WhenStartDateIsAfterEndDate()
        {
            var model = new CreateEventInputModel
            {
                Name = "Invalid Event",
                Description = "Invalid Dates",
                StartDate = DateTime.UtcNow.AddDays(2),
                EndDate = DateTime.UtcNow.AddDays(1),
                Capacity = 10,
                CategoryId = Guid.NewGuid()
            };

            var result = await _eventsService.CreateEventAsync(model, Guid.NewGuid());

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Does.Contain(StartDateBeforeEndDate));
        }

        [Test]
        public async Task JoinEventAsync_ReturnsFailure_WhenEventIsFull()
        {
            var userId = Guid.NewGuid();
            var eventId = Guid.NewGuid();
            var eventEntity = new Event
            {
                EventId = eventId,
                Capacity = 2,
                NumberOfParticipants = 2
            };

            _eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId)).ReturnsAsync(eventEntity);
            _userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(new ApplicationUser { Id = userId });

            var result = await _eventsService.JoinEventAsync(eventId, userId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Does.Contain(EventFull));
        }

        [Test]
        public async Task LeaveEventAsync_ReturnsFailure_WhenUserNotJoinedEvent()
        {
            var userId = Guid.NewGuid();
            var eventId = Guid.NewGuid();

            _userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(new ApplicationUser { Id = userId });
            _attendanceRepositoryMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<UserEventAttendance, bool>>>()))
                .ReturnsAsync((UserEventAttendance)null);

            var result = await _eventsService.LeaveEventAsync(eventId, userId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Does.Contain(UserNotJoinedEvent));
        }

        [Test]
        public async Task LoadEventsAsync_FiltersCorrectly_BySearchTerm()
        {
            var events = new List<Event>
            {
                new Event { EventId = Guid.NewGuid(), Name = "Test Event", isDeleted = false },
                new Event { EventId = Guid.NewGuid(), Name = "Another Event", isDeleted = false }
            };

            _eventRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(events);
            _categoryRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Category>());

            var searchTerm = "Test Event";

            var result = await _eventsService.LoadEventsAsync(searchTerm, null, null, 1, 10);

            Assert.That(result.Data.Events.Count, Is.EqualTo(1));
            Assert.That(result.Data.Events.First().Name, Is.EqualTo("Test Event"));
        }

        [Test]
        public async Task EditEventAsync_UpdatesEventSuccessfully_WithImage()
        {
            var eventId = Guid.NewGuid();
            var existingEvent = new Event
            {
                EventId = eventId,
                Name = "Old Event",
                ImageUrl = null
            };

            var model = new EditEventInputModel
            {
                EventId = eventId,
                Name = "Updated Event",
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Image = Mock.Of<IFormFile>(f => f.FileName == "image.jpg")
            };

            var imagePath = "uploaded_image_url";

            var mockSection = new Mock<IConfigurationSection>();
            mockSection.Setup(x => x.Value).Returns("true");

            _configurationMock
                .Setup(x => x.GetSection("UseAzureBlobStorage"))
                .Returns(mockSection.Object);

            _eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId)).ReturnsAsync(existingEvent);
            _fileStorageServiceMock
                .Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(imagePath);

            var result = await _eventsService.EditEventAsync(model);

            Assert.That(result.Succeeded, Is.True);
            _eventRepositoryMock.Verify(x => x.UpdateAsync(existingEvent), Times.Once);
        }

        [Test]
        public async Task EditEventAsync_ReturnsFailure_WhenEventNotFound()
        {
            var model = new EditEventInputModel
            {
                EventId = Guid.NewGuid(),
                Name = "Updated Event",
                Description = "Updated Description",
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Capacity = 50,
                Address = "New Address",
                CategoryId = Guid.NewGuid()
            };

            _eventRepositoryMock.Setup(x => x.GetByIdAsync(model.EventId)).ReturnsAsync((Event)null);

            var result = await _eventsService.EditEventAsync(model);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(EventsServiceErrorMessages.EventNotFound));
        }

        [Test]
        public async Task LeaveEventAsync_DecreasesParticipantsCount_WhenSuccessful()
        {
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var attendance = new UserEventAttendance { EventId = eventId, UserId = userId };
            var eventEntity = new Event { EventId = eventId, NumberOfParticipants = 2 };

            _userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(new ApplicationUser { Id = userId });
            _attendanceRepositoryMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<UserEventAttendance, bool>>>()))
                .ReturnsAsync(attendance);
            _eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId)).ReturnsAsync(eventEntity);

            var result = await _eventsService.LeaveEventAsync(eventId, userId);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(eventEntity.NumberOfParticipants, Is.EqualTo(1));
            _attendanceRepositoryMock.Verify(x => x.DeleteAsync(attendance), Times.Once);
        }

        [Test]
        public async Task LeaveEventAsync_ReturnsFailure_WhenUserNotJoined()
        {
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(new ApplicationUser { Id = userId });
            _attendanceRepositoryMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<UserEventAttendance, bool>>>()))
                .ReturnsAsync((UserEventAttendance)null);

            var result = await _eventsService.LeaveEventAsync(eventId, userId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(EventsServiceErrorMessages.UserNotJoinedEvent));
        }

        [Test]
        public async Task SoftDeleteCommentAsync_SoftDeletesComment_WhenAuthorized()
        {
            var commentId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var comment = new Comment { CommentId = commentId, UserId = userId, isDeleted = false };

            _commentRepositoryMock.Setup(x => x.GetByIdAsync(commentId)).ReturnsAsync(comment);
            _userAuthServiceMock.Setup(x => x.IsUserInRoleAsync(userId.ToString(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var result = await _eventsService.SoftDeleteCommentAsync(commentId, userId);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(comment.isDeleted, Is.True);
            _commentRepositoryMock.Verify(x => x.UpdateAsync(comment), Times.Once);
        }

        [Test]
        public async Task SoftDeleteCommentAsync_ReturnsFailure_WhenUnauthorized()
        {
            var commentId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var comment = new Comment { CommentId = commentId, UserId = Guid.NewGuid(), isDeleted = false };

            _commentRepositoryMock.Setup(x => x.GetByIdAsync(commentId)).ReturnsAsync(comment);
            _userAuthServiceMock.Setup(x => x.IsUserInRoleAsync(userId.ToString(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var result = await _eventsService.SoftDeleteCommentAsync(commentId, userId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(EventsServiceErrorMessages.UnauthorizedCommentDeletion));
        }

        [Test]
        public async Task GetEditEventAsync_ReturnsEventDetails()
        {
            var eventId = Guid.NewGuid();
            var eventEntity = new Event
            {
                EventId = eventId,
                Name = "Test Event",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1)
            };

            _eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId)).ReturnsAsync(eventEntity);
            _categoryRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Category>());

            var result = await _eventsService.GetEditEventAsync(eventId);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Data.Name, Is.EqualTo("Test Event"));
        }

        [Test]
        public async Task GetEditEventAsync_ReturnsFailure_WhenEventNotFound()
        {
            var eventId = Guid.NewGuid();
            _eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId)).ReturnsAsync((Event)null);

            var result = await _eventsService.GetEditEventAsync(eventId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(EventsServiceErrorMessages.EventNotFound));
        }

        [Test]
        public async Task LoadMyEventsAsync_ReturnsEventsForUser()
        {
            var userId = Guid.NewGuid();
            var events = new List<Event>
            {
                new Event { EventId = Guid.NewGuid(), OrganizerID = userId, isDeleted = false },
                new Event { EventId = Guid.NewGuid(), OrganizerID = userId, isDeleted = false }
            };

            _eventRepositoryMock.Setup(x => x.GetAllAttached()).Returns(events.AsQueryable().BuildMock());
            _categoryRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Category>());

            var result = await _eventsService.LoadMyEventsAsync("", null, "", userId.ToString(), 1, 10);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Data.Events.Count, Is.EqualTo(2));
        }
    }
}
