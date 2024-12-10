using MockQueryable;
using Moq;

using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data;
using STAREvents.Services.Data.Interfaces;

namespace STAREvents.Services.Tests
{
    [TestFixture]
    public class MyEventsServiceTests
    {
        private Mock<IRepository<Event, object>> eventRepositoryMock;
        private Mock<IRepository<Category, object>> categoryRepositoryMock;
        private Mock<IUserAuthService> userAuthServiceMock;
        private Mock<IRepository<UserEventAttendance, object>> attendanceRepositoryMock;
        private MyEventsService myEventsService;

        [SetUp]
        public void SetUp()
        {
            eventRepositoryMock = new Mock<IRepository<Event, object>>();
            categoryRepositoryMock = new Mock<IRepository<Category, object>>();
            userAuthServiceMock = new Mock<IUserAuthService>();
            attendanceRepositoryMock = new Mock<IRepository<UserEventAttendance, object>>();

            myEventsService = new MyEventsService(
                eventRepositoryMock.Object,
                categoryRepositoryMock.Object,
                attendanceRepositoryMock.Object,
                userAuthServiceMock.Object
            );
        }

        [Test]
        public async Task LoadMyEventsAsync_UserNotFound_ThrowsKeyNotFoundException()
        {
            Guid userId = Guid.NewGuid();
            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync((ApplicationUser)null);

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await myEventsService.LoadMyEventsAsync("", null, "Recent", userId.ToString())
            );

            Assert.That(ex.Message, Is.EqualTo("User not found"));
        }

        [Test]
        public async Task LoadMyEventsAsync_ReturnsCorrectEventCount()
        {
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId };
            var category = new Category { CategoryID = Guid.NewGuid(), Name = "Music" };
            var events = new List<Event>
        {
            new Event
            {
                Name = "Event 1",
                Description = "Description 1",
                ImageUrl = "http://example.com/image1.jpg",
                Address = "Address 1",
                CreatedOnDate = DateTime.UtcNow,
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Capacity = 100,
                NumberOfParticipants = 10,
                isDeleted = false,
                OrganizerID = userId,
                Organizer = user,
                CategoryID = category.CategoryID,
                Category = category
            },
            new Event
            {
                Name = "Event 2",
                Description = "Description 2",
                ImageUrl = "http://example.com/image2.jpg",
                Address = "Address 2",
                CreatedOnDate = DateTime.UtcNow,
                StartDate = DateTime.UtcNow.AddDays(3),
                EndDate = DateTime.UtcNow.AddDays(4),
                Capacity = 200,
                NumberOfParticipants = 20,
                isDeleted = false,
                OrganizerID = userId,
                Organizer = user,
                CategoryID = category.CategoryID,
                Category = category
            }
        };

            var mockEventQueryable = events.AsQueryable().BuildMock();

            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(user);

            eventRepositoryMock.Setup(x => x.GetAllAttached())
                .Returns(mockEventQueryable);

            categoryRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Category> { category });

            var result = await myEventsService.LoadMyEventsAsync("", null, "Recent", userId.ToString());

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Events.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task LoadMyEventsAsync_FiltersEventsBySearchTerm()
        {
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId };
            var category = new Category { CategoryID = Guid.NewGuid(), Name = "Music" };
            var events = new List<Event>
        {
            new Event
            {
                Name = "Expected Event",
                Description = "Description 1",
                ImageUrl = "http://example.com/image1.jpg",
                Address = "Address 1",
                CreatedOnDate = DateTime.UtcNow,
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Capacity = 100,
                NumberOfParticipants = 10,
                isDeleted = false,
                OrganizerID = userId,
                Organizer = user,
                CategoryID = category.CategoryID,
                Category = category
            },
            new Event
            {
                Name = "Nonmatching Event",
                Description = "Description 2",
                ImageUrl = "http://example.com/image2.jpg",
                Address = "Address 2",
                CreatedOnDate = DateTime.UtcNow,
                StartDate = DateTime.UtcNow.AddDays(3),
                EndDate = DateTime.UtcNow.AddDays(4),
                Capacity = 200,
                NumberOfParticipants = 20,
                isDeleted = false,
                OrganizerID = userId,
                Organizer = user,
                CategoryID = category.CategoryID,
                Category = category
            }
        };

            var mockEventQueryable = events.AsQueryable().BuildMock();

            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(user);

            eventRepositoryMock.Setup(x => x.GetAllAttached())
                .Returns(mockEventQueryable);

            categoryRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Category> { category });

            var result = await myEventsService.LoadMyEventsAsync("Expected", null, "Recent", userId.ToString());

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Events.Count, Is.EqualTo(1));
            Assert.That(result.Events.First().Name, Is.EqualTo("Expected Event"));
        }

        [Test]
        public async Task LoadMyEventsAsync_SortsEventsCorrectly()
        {
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId };
            var category = new Category { CategoryID = Guid.NewGuid(), Name = "Music" };
            var events = new List<Event>
        {
            new Event
            {
                Name = "Event A",
                Description = "Description 1",
                ImageUrl = "http://example.com/image1.jpg",
                Address = "Address 1",
                CreatedOnDate = DateTime.UtcNow,
                StartDate = DateTime.UtcNow.AddDays(2),
                EndDate = DateTime.UtcNow.AddDays(3),
                Capacity = 100,
                NumberOfParticipants = 10,
                isDeleted = false,
                OrganizerID = userId,
                Organizer = user,
                CategoryID = category.CategoryID,
                Category = category
            },
            new Event
            {
                Name = "Event B",
                Description = "Description 2",
                ImageUrl = "http://example.com/image2.jpg",
                Address = "Address 2",
                CreatedOnDate = DateTime.UtcNow,
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Capacity = 200,
                NumberOfParticipants = 20,
                isDeleted = false,
                OrganizerID = userId,
                Organizer = user,
                CategoryID = category.CategoryID,
                Category = category
            }
        };

            var mockEventQueryable = events.AsQueryable().BuildMock();

            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(user);

            eventRepositoryMock.Setup(x => x.GetAllAttached())
                .Returns(mockEventQueryable);

            categoryRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Category> { category });

            var result = await myEventsService.LoadMyEventsAsync("", null, "Recent", userId.ToString());

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Events.Count, Is.EqualTo(2));
            Assert.That(result.Events.First().Name, Is.EqualTo("Event A"));
        }
    }
}

