using MockQueryable;
using Moq;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data;
using STAREvents.Services.Data.Interfaces;

using static STAREvents.Common.EntityValidationConstants.RoleNames;
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
        public async Task GetAdminDashboardViewModelAsync_ReturnsCorrectData()
        {
            var events = new List<Event>
        {
            new Event { EventId = Guid.NewGuid(), Name = "Event 1", StartDate = DateTime.UtcNow.AddDays(1), EndDate = DateTime.UtcNow.AddDays(2) },
            new Event { EventId = Guid.NewGuid(), Name = "Event 2", StartDate = DateTime.UtcNow.AddDays(-2), EndDate = DateTime.UtcNow.AddDays(-1) }
        };

            var users = new List<ApplicationUser>
        {
            new ApplicationUser { Id = Guid.NewGuid(), UserName = "User1", isDeleted = false },
            new ApplicationUser { Id = Guid.NewGuid(), UserName = "User2", isDeleted = false }
        };

            var mockEventQueryable = events.AsQueryable().BuildMock();
            eventRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(events);
            eventRepositoryMock.Setup(x => x.GetAllAttached()).Returns(mockEventQueryable);
            eventRepositoryMock.Setup(x => x.GetAllAttached()).Returns(mockEventQueryable);
            userAuthServiceMock.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(users);

            var result = await adminService.GetAdminDashboardViewModelAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.TotalEvents, Is.EqualTo(2));
            Assert.That(result.UpcomingEvents, Is.EqualTo(1));
            Assert.That(result.PastEvents, Is.EqualTo(1));
            Assert.That(result.TotalUsers, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllEventsAsync_ReturnsCorrectEventList()
        {
            var events = new List<Event>
        {
            new Event { EventId = Guid.NewGuid(), Name = "Event 1", Organizer = new ApplicationUser { Id = Guid.NewGuid() }, isDeleted = false },
            new Event { EventId = Guid.NewGuid(), Name = "Event 2", Organizer = new ApplicationUser { Id = Guid.NewGuid() }, isDeleted = true }
        };

            var mockEventQueryable = events.AsQueryable().BuildMock();
            eventRepositoryMock.Setup(x => x.GetAllAttached()).Returns(mockEventQueryable);

            var result = await adminService.GetAllEventsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllUsersAsync_ReturnsCorrectUserList()
        {
            var users = new List<ApplicationUser>
        {
            new ApplicationUser { Id = Guid.NewGuid(), UserName = "User1", Email = "user1@test.com", isDeleted = false },
            new ApplicationUser { Id = Guid.NewGuid(), UserName = "User2", Email = "user2@test.com", isDeleted = true }
        };

            userAuthServiceMock.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(users);
            userAuthServiceMock.Setup(x => x.IsUserInRoleAsync(It.IsAny<string>(), Administrator)).ReturnsAsync(false);

            var result = await adminService.GetAllUsersAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task RecoverEventAsync_UpdatesEventSuccessfully()
        {
            var eventId = Guid.NewGuid();
            var eventEntity = new Event { EventId = eventId, isDeleted = true };

            eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId)).ReturnsAsync(eventEntity);

            await adminService.RecoverEventAsync(eventId);

            Assert.That(eventEntity.isDeleted, Is.False);
            eventRepositoryMock.Verify(x => x.UpdateAsync(eventEntity), Times.Once);
        }

        [Test]
        public async Task SoftDeleteEventAsync_UpdatesEventSuccessfully()
        {
            var eventId = Guid.NewGuid();
            var eventEntity = new Event { EventId = eventId, isDeleted = false };

            eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId)).ReturnsAsync(eventEntity);

            await adminService.SoftDeleteEventAsync(eventId);

            Assert.That(eventEntity.isDeleted, Is.True);
            eventRepositoryMock.Verify(x => x.UpdateAsync(eventEntity), Times.Once);
        }

        [Test]
        public async Task RecoverUserAsync_UpdatesUserSuccessfully()
        {
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId, isDeleted = true };

            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString())).ReturnsAsync(user);

            await adminService.RecoverUserAsync(userId);

            Assert.That(user.isDeleted, Is.False);
            userAuthServiceMock.Verify(x => x.UpdateUserAsync(user), Times.Once);
        }

        [Test]
        public async Task SoftDeleteUserAsync_UpdatesUserSuccessfully()
        {
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId, isDeleted = false };

            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString())).ReturnsAsync(user);

            await adminService.SoftDeleteUserAsync(userId);

            Assert.That(user.isDeleted, Is.True);
            userAuthServiceMock.Verify(x => x.UpdateUserAsync(user), Times.Once);
        }

        [Test]
        public async Task AddAdminRole_AssignsRoleSuccessfully()
        {
            var userId = Guid.NewGuid();

            await adminService.AddAdminRole(userId);

            userAuthServiceMock.Verify(x => x.AddRoleToUserAsync(userId.ToString(), Administrator), Times.Once);
        }

        [Test]
        public async Task RemoveAdminRole_RemovesRoleSuccessfully()
        {
            var userId = Guid.NewGuid();

            await adminService.RemoveAdminRole(userId);

            userAuthServiceMock.Verify(x => x.RemoveRoleFromUserAsync(userId.ToString(), Administrator), Times.Once);
        }
    }

}
