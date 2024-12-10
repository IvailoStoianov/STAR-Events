using System.Linq.Expressions;

using Microsoft.AspNetCore.Hosting;
using MockQueryable;
using Moq;

using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;

using static STAREvents.Common.EntityValidationConstants.RoleNames;

namespace STAREvents.Services.Tests
{
    [TestFixture]
    public class EventsServiceTests
    {
        private Mock<IRepository<Event, object>> eventRepositoryMock;
        private Mock<IRepository<Category, object>> categoryRepositoryMock;
        private Mock<IRepository<Comment, object>> commentRepositoryMock;
        private Mock<IRepository<UserEventAttendance, object>> attendanceRepositoryMock;
        private Mock<IUserAuthService> userAuthServiceMock;
        private Mock<IWebHostEnvironment> webHostEnvironmentMock;
        private EventsService eventsService;

        [SetUp]
        public void SetUp()
        {
            eventRepositoryMock = new Mock<IRepository<Event, object>>();
            categoryRepositoryMock = new Mock<IRepository<Category, object>>();
            commentRepositoryMock = new Mock<IRepository<Comment, object>>();
            attendanceRepositoryMock = new Mock<IRepository<UserEventAttendance, object>>();
            userAuthServiceMock = new Mock<IUserAuthService>();
            webHostEnvironmentMock = new Mock<IWebHostEnvironment>();

            eventsService = new EventsService(
                eventRepositoryMock.Object,
                categoryRepositoryMock.Object,
                commentRepositoryMock.Object,
                attendanceRepositoryMock.Object,
                userAuthServiceMock.Object,
                webHostEnvironmentMock.Object
            );
        }

        [Test]
        public async Task LoadEventAsync_ReturnsCorrectEventCount()
        {
            var category = new Category { CategoryID = Guid.NewGuid(), Name = "Music" };
            var events = new List<Event>
        {
            new Event
            {
                EventId = Guid.NewGuid(),
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
                OrganizerID = Guid.NewGuid(),
                Organizer = new ApplicationUser { Id = Guid.NewGuid() },
                CategoryID = category.CategoryID,
                Category = category
            },
            new Event
            {
                EventId = Guid.NewGuid(),
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
                OrganizerID = Guid.NewGuid(),
                Organizer = new ApplicationUser { Id = Guid.NewGuid() },
                CategoryID = category.CategoryID,
                Category = category
            }
        };

            var mockEventQueryable = events.AsQueryable().BuildMock();
            eventRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(events);
            categoryRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Category> { category });

            var result = await eventsService.LoadEventAsync("", null, "Recent");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Events.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetEventDetailsAsync_ReturnsCorrectEventDetails()
        {
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId };
            var category = new Category { CategoryID = Guid.NewGuid(), Name = "Music" };
            var eventEntity = new Event
            {
                EventId = eventId,
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
                Category = category,
                EventComments = new List<Comment>
            {
                new Comment
                {
                    CommentId = Guid.NewGuid(),
                    Content = "Test Comment",
                    PostedDate = DateTime.UtcNow,
                    User = new ApplicationUser { Id = Guid.NewGuid() }
                }
            }
            };

            var mockEventQueryable = new List<Event> { eventEntity }.AsQueryable().BuildMock();

            eventRepositoryMock.Setup(x => x.GetAllAttached()).Returns(mockEventQueryable);
            userAuthServiceMock.Setup(x => x.GetUserByNameAsync(It.IsAny<string>())).ReturnsAsync(user);

            var result = await eventsService.GetEventDetailsAsync(eventId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Event 1"));
            Assert.That(result.Comments.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task AddCommentAsync_AddsCommentSuccessfully()
        {
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId };

            userAuthServiceMock.Setup(x => x.GetUserByNameAsync(It.IsAny<string>())).ReturnsAsync(user);

            await eventsService.AddCommentAsync(eventId, "TestUser", "Test Comment");

            commentRepositoryMock.Verify(x => x.AddAsync(It.Is<Comment>(c =>
                c.EventId == eventId &&
                c.UserId == userId &&
                c.Content == "Test Comment")), Times.Once);
        }

        [Test]
        public async Task SoftDeleteCommentAsync_DeletesCommentSuccessfully()
        {
            var commentId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var comment = new Comment
            {
                CommentId = commentId,
                UserId = userId,
                isDeleted = false
            };

            commentRepositoryMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Comment, bool>>>()))
            .ReturnsAsync(comment);
            userAuthServiceMock.Setup(x => x.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { Id = userId });
            userAuthServiceMock.Setup(x => x.IsUserInRoleAsync(It.IsAny<string>(), Administrator)).ReturnsAsync(true);

            await eventsService.SoftDeleteCommentAsync(commentId, userId);

            Assert.That(comment.isDeleted, Is.True);
            commentRepositoryMock.Verify(x => x.UpdateAsync(comment), Times.Once);
        }
    }

}