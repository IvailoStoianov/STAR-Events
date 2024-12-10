using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Moq;

using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data;
using STAREvents.Web.ViewModels.CreateEvents;

using static STAREvents.Common.EntityValidationConstants;
using static STAREvents.Common.ErrorMessagesConstants;
using static STAREvents.Common.FilePathConstants.EventPicturePaths;

namespace STAREvents.Services.Tests
{


    [TestFixture]
    public class CreateEventsServiceTests
    {
        private Mock<IRepository<Event, object>> eventRepositoryMock;
        private Mock<IRepository<Category, object>> categoryRepositoryMock;
        private Mock<IWebHostEnvironment> webHostEnvironmentMock;
        private CreateEventsService createEventsService;

        [SetUp]
        public void SetUp()
        {
            eventRepositoryMock = new Mock<IRepository<Event, object>>();
            categoryRepositoryMock = new Mock<IRepository<Category, object>>();
            webHostEnvironmentMock = new Mock<IWebHostEnvironment>();

            createEventsService = new CreateEventsService(
                eventRepositoryMock.Object,
                webHostEnvironmentMock.Object,
                categoryRepositoryMock.Object
            );
        }

        [Test]
        public async Task LoadCategoriesAsync_ReturnsCategories()
        {
            var categories = new List<Category>
        {
            new Category { CategoryID = Guid.NewGuid(), Name = "Music" },
            new Category { CategoryID = Guid.NewGuid(), Name = "Sports" }
        };

            categoryRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(categories);

            var result = await createEventsService.LoadCategoriesAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task CreateEventAsync_WithNullModel_ThrowsArgumentNullException()
        {
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await createEventsService.CreateEventAsync(null, Guid.NewGuid())
            );

            Assert.That(ex.ParamName, Is.EqualTo("model"));
        }

        [Test]
        public async Task CreateEventAsync_WithImageSizeExceeded_ThrowsInvalidOperationException()
        {
            var model = new CreateEventInputModel
            {
                Name = "Test Event",
                Description = "Description",
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Capacity = 100,
                Image = Mock.Of<IFormFile>(x => x.Length == EventConstants.MaxImageSize + 1),
                CategoryId = Guid.NewGuid(),
                Address = "Test Address"
            };

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await createEventsService.CreateEventAsync(model, Guid.NewGuid())
            );

            Assert.That(ex.Message, Is.EqualTo(string.Format(CreateEventsServiceErrorMessages.ImageSizeExceeded, EventConstants.MaxImageSize)));
        }

        [Test]
        public async Task CreateEventAsync_WithValidModel_AddsEventSuccessfully()
        {
            var userId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var model = new CreateEventInputModel
            {
                Name = "Test Event",
                Description = "Test Description",
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Capacity = 100,
                Image = Mock.Of<IFormFile>(x => x.FileName == "test.jpg" && x.Length == 1024),
                CategoryId = categoryId,
                Address = "Test Address"
            };

            webHostEnvironmentMock.Setup(x => x.WebRootPath)
                .Returns("wwwroot");

            var resultPath = Path.Combine("wwwroot", $"{DefaultEventPicturePath}/test.jpg");
            eventRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Event>()))
                .Returns(Task.CompletedTask);

            await createEventsService.CreateEventAsync(model, userId);

            eventRepositoryMock.Verify(x => x.AddAsync(It.Is<Event>(e =>
                e.Name == model.Name &&
                e.Description == model.Description &&
                e.StartDate == model.StartDate &&
                e.EndDate == model.EndDate &&
                e.Capacity == model.Capacity &&
                e.ImageUrl.Contains("test.jpg") &&
                e.OrganizerID == userId &&
                e.CategoryID == categoryId &&
                e.Address == model.Address)), Times.Once);
        }

        [Test]
        public void CreateEventAsync_EventRepositoryThrowsException_ThrowsInvalidOperationException()
        {
            var userId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var model = new CreateEventInputModel
            {
                Name = "Test Event",
                Description = "Test Description",
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Capacity = 100,
                CategoryId = categoryId,
                Address = "Test Address"
            };

            eventRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Event>()))
                .Throws(new Exception("Test exception"));

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await createEventsService.CreateEventAsync(model, userId)
            );

            Assert.That(ex.Message, Is.EqualTo(CreateEventsServiceErrorMessages.EventCreationError));
            Assert.That(ex.InnerException.Message, Is.EqualTo("Test exception"));
        }
    }

}
