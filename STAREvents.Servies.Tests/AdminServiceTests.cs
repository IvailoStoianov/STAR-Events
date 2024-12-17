using Microsoft.AspNetCore.Identity;
using MockQueryable;
using MockQueryable.Moq;
using Moq;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data;
using STAREvents.Services.Data.Interfaces;
using static STAREvents.Common.EntityValidationConstants.RoleNames;
using static STAREvents.Common.ErrorMessagesConstants.AdminServiceErrorMessages;
using static STAREvents.Common.ErrorMessagesConstants.EventsServiceErrorMessages;


namespace STAREvents.Services.Tests
{
    [TestFixture]
    public class AdminServiceTests
    {
        private Mock<IRepository<Event, object>> eventRepositoryMock;
        private Mock<IUserAuthService> userAuthServiceMock;
        private AdminService adminService;

        [SetUp]
        public void SetUp()
        {
            eventRepositoryMock = new Mock<IRepository<Event, object>>();
            userAuthServiceMock = new Mock<IUserAuthService>();
            adminService = new AdminService(eventRepositoryMock.Object, userAuthServiceMock.Object);
        }

        [Test]
        public async Task GetAdminDashboardViewModelAsync_ReturnsSuccess()
        {
            // Arrange
            var events = new List<Event>
                {
                    new Event { EventId = Guid.NewGuid(), StartDate = DateTime.UtcNow.AddDays(1), EndDate = DateTime.UtcNow.AddDays(2) },
                    new Event { EventId = Guid.NewGuid(), StartDate = DateTime.UtcNow.AddDays(-2), EndDate = DateTime.UtcNow.AddDays(-1) }
                };

            var users = new List<ApplicationUser>
                {
                    new ApplicationUser { Id = Guid.NewGuid(), UserName = "User1", isDeleted = false },
                    new ApplicationUser { Id = Guid.NewGuid(), UserName = "User2", isDeleted = false }
                };


            eventRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(events);


            eventRepositoryMock.Setup(x => x.GetAllAttached())
                .Returns(events.AsQueryable().BuildMockDbSet().Object);

            userAuthServiceMock.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(users);

            var result = await adminService.GetAdminDashboardViewModelAsync();

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Data.TotalEvents, Is.EqualTo(2));
            Assert.That(result.Data.PastEvents, Is.EqualTo(2));
            Assert.That(result.Data.TotalUsers, Is.EqualTo(2));
        }


        [Test]
        public async Task SoftDeleteEventAsync_ReturnsSuccess_WhenEventExists()
        {
            var eventId = Guid.NewGuid();
            var eventEntity = new Event { EventId = eventId, isDeleted = false };

            eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId)).ReturnsAsync(eventEntity);

            var result = await adminService.SoftDeleteEventAsync(eventId);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(eventEntity.isDeleted, Is.True);
            eventRepositoryMock.Verify(x => x.UpdateAsync(eventEntity), Times.Once);
        }

        [Test]
        public async Task SoftDeleteEventAsync_ReturnsFailure_WhenEventNotFound()
        {
            var eventId = Guid.NewGuid();
            eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId)).ReturnsAsync((Event)null);

            var result = await adminService.SoftDeleteEventAsync(eventId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(EventNotFound));
        }

        [Test]
        public async Task RecoverEventAsync_ReturnsSuccess_WhenEventExists()
        {
            var eventId = Guid.NewGuid();
            var eventEntity = new Event { EventId = eventId, isDeleted = true };

            eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId)).ReturnsAsync(eventEntity);

            var result = await adminService.RecoverEventAsync(eventId);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(eventEntity.isDeleted, Is.False);
            eventRepositoryMock.Verify(x => x.UpdateAsync(eventEntity), Times.Once);
        }

        [Test]
        public async Task RecoverEventAsync_ReturnsFailure_WhenEventNotFound()
        {
            var eventId = Guid.NewGuid();
            eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId)).ReturnsAsync((Event)null);

            var result = await adminService.RecoverEventAsync(eventId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(EventNotFound));
        }

        [Test]
        public async Task SoftDeleteUserAsync_ReturnsSuccess_WhenUserExists()
        {
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId, isDeleted = false };

            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString())).ReturnsAsync(user);

            var result = await adminService.SoftDeleteUserAsync(userId);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(user.isDeleted, Is.True);
            userAuthServiceMock.Verify(x => x.UpdateUserAsync(user), Times.Once);
        }

        [Test]
        public async Task SoftDeleteUserAsync_ReturnsFailure_WhenUserNotFound()
        {
            var userId = Guid.NewGuid();
            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString())).ReturnsAsync((ApplicationUser)null);

            var result = await adminService.SoftDeleteUserAsync(userId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(UserNotFound));
        }

        [Test]
        public async Task AddAdminRole_ReturnsSuccess()
        {
            var userId = Guid.NewGuid();

            var result = await adminService.AddAdminRole(userId);

            Assert.That(result.Succeeded, Is.True);
            userAuthServiceMock.Verify(x => x.AddRoleToUserAsync(userId.ToString(), Administrator), Times.Once);
        }

        [Test]
        public async Task RemoveAdminRole_ReturnsSuccess()
        {
            var userId = Guid.NewGuid();

            var result = await adminService.RemoveAdminRole(userId);

            Assert.That(result.Succeeded, Is.True);
            userAuthServiceMock.Verify(x => x.RemoveRoleFromUserAsync(userId.ToString(), Administrator), Times.Once);
        }
        [Test]
        public async Task RecoverUserAsync_ReturnsSuccess_WhenUserExists()
        {
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId, isDeleted = true };

            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString())).ReturnsAsync(user);
            userAuthServiceMock.Setup(x => x.UpdateUserAsync(user)).ReturnsAsync(IdentityResult.Success);

            var result = await adminService.RecoverUserAsync(userId);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(user.isDeleted, Is.False);
            userAuthServiceMock.Verify(x => x.UpdateUserAsync(user), Times.Once);
        }

        [Test]
        public async Task RecoverUserAsync_ReturnsFailure_WhenUserNotFound()
        {
            var userId = Guid.NewGuid();
            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString())).ReturnsAsync((ApplicationUser)null);

            var result = await adminService.RecoverUserAsync(userId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(UserNotFound));
        }

        [Test]
        public async Task GetEventCommentsAsync_ReturnsComments_WhenEventExists()
        {
            var eventId = Guid.NewGuid();
            var eventItem = new Event
            {
                EventId = eventId,
                EventComments = new List<Comment>
        {
            new Comment { CommentId = Guid.NewGuid(), Content = "Test Comment 1", User = new ApplicationUser() },
            new Comment { CommentId = Guid.NewGuid(), Content = "Test Comment 2", User = new ApplicationUser() }
        }
            };

            eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId)).ReturnsAsync(eventItem);

            var result = await adminService.GetEventCommentsAsync(eventId);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Data.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetEventCommentsAsync_ReturnsFailure_WhenEventNotFound()
        {
            var eventId = Guid.NewGuid();
            eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId)).ReturnsAsync((Event)null);

            var result = await adminService.GetEventCommentsAsync(eventId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(EventNotFound));
        }

        [Test]
        public async Task SoftDeleteCommentAsync_ReturnsSuccess_WhenAuthorized()
        {
            var commentId = Guid.NewGuid();
            var userName = "testUser";
            var userId = Guid.NewGuid();

            var user = new ApplicationUser { Id = userId, UserName = userName };
            var eventWithComment = new Event
            {
                EventId = Guid.NewGuid(),
                OrganizerID = userId,
                EventComments = new List<Comment>
        {
            new Comment { CommentId = commentId, UserId = userId, isDeleted = false }
        }
            };

            userAuthServiceMock.Setup(x => x.GetUserByNameAsync(userName)).ReturnsAsync(user);
            eventRepositoryMock.Setup(x => x.GetAllAttached())
                .Returns(new List<Event> { eventWithComment }.AsQueryable().BuildMock());
            userAuthServiceMock.Setup(x => x.IsUserInRoleAsync(user.Id.ToString(), Administrator)).ReturnsAsync(false);

            var result = await adminService.SoftDeleteCommentAsync(commentId, userName);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(eventWithComment.EventComments.First().isDeleted, Is.True);
            eventRepositoryMock.Verify(x => x.UpdateAsync(eventWithComment), Times.Once);
        }

        [Test]
        public async Task SoftDeleteCommentAsync_ReturnsFailure_WhenCommentNotFound()
        {
            var commentId = Guid.NewGuid();
            var userName = "testUser";

            var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = userName };

            userAuthServiceMock.Setup(x => x.GetUserByNameAsync(userName)).ReturnsAsync(user);
            eventRepositoryMock.Setup(x => x.GetAllAttached()).Returns(new List<Event>().AsQueryable().BuildMock());

            var result = await adminService.SoftDeleteCommentAsync(commentId, userName);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(EventNotFound));
        }

        [Test]
        public async Task SoftDeleteCommentAsync_ReturnsFailure_WhenUserUnauthorized()
        {
            var commentId = Guid.NewGuid();
            var userName = "testUser";
            var userId = Guid.NewGuid();

            var user = new ApplicationUser { Id = userId, UserName = userName };
            var eventWithComment = new Event
            {
                EventId = Guid.NewGuid(),
                OrganizerID = Guid.NewGuid(),
                EventComments = new List<Comment>
        {
            new Comment { CommentId = commentId, UserId = Guid.NewGuid(), isDeleted = false }
        }
            };

            userAuthServiceMock.Setup(x => x.GetUserByNameAsync(userName)).ReturnsAsync(user);
            eventRepositoryMock.Setup(x => x.GetAllAttached())
                .Returns(new List<Event> { eventWithComment }.AsQueryable().BuildMock());
            userAuthServiceMock.Setup(x => x.IsUserInRoleAsync(user.Id.ToString(), Administrator)).ReturnsAsync(false);

            var result = await adminService.SoftDeleteCommentAsync(commentId, userName);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(NotAllowedToDeleteComment));
        }

    }
}
