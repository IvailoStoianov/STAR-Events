using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Moq;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.CreateEvents;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static STAREvents.Common.EntityValidationConstants;
using static STAREvents.Common.ErrorMessagesConstants;
using static STAREvents.Common.FilePathConstants.EventPicturePaths;
using static STAREvents.Common.FilePathConstants.AzureContainerNames;

namespace STAREvents.Services.Tests
{
    [TestFixture]
    public class CreateEventsServiceTests
    {
        private Mock<IRepository<Event, object>> eventRepositoryMock;
        private Mock<IRepository<Category, object>> categoryRepositoryMock;
        private Mock<IFileStorageService> fileStorageServiceMock;
        private Mock<IConfiguration> configurationMock;
        private CreateEventsService createEventsService;

        [SetUp]
        public void SetUp()
        {
            eventRepositoryMock = new Mock<IRepository<Event, object>>();
            categoryRepositoryMock = new Mock<IRepository<Category, object>>();
            fileStorageServiceMock = new Mock<IFileStorageService>();
            configurationMock = new Mock<IConfiguration>();

            var sectionMock = new Mock<IConfigurationSection>();
            sectionMock.Setup(s => s.Value).Returns("false"); // Default to local saving
            configurationMock.Setup(c => c.GetSection("UseAzureBlobStorage")).Returns(sectionMock.Object);

            createEventsService = new CreateEventsService(
                eventRepositoryMock.Object,
                categoryRepositoryMock.Object,
                fileStorageServiceMock.Object,
                configurationMock.Object
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
        public async Task CreateEventAsync_WithValidModel_UsesLocalSaving()
        {
            var sectionMock = new Mock<IConfigurationSection>();
            sectionMock.Setup(s => s.Value).Returns("false"); // Test local saving
            configurationMock.Setup(c => c.GetSection("UseAzureBlobStorage")).Returns(sectionMock.Object);

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

            fileStorageServiceMock.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>(), DefaultEventPicturePath))
                .ReturnsAsync("/images/events/test.jpg");

            eventRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Event>()))
                .Returns(Task.CompletedTask);

            await createEventsService.CreateEventAsync(model, userId);

            fileStorageServiceMock.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>(), DefaultEventPicturePath), Times.Once);
            eventRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Event>()), Times.Once);
        }

        [Test]
        public async Task CreateEventAsync_WithValidModel_UsesAzureBlobStorage()
        {
            var sectionMock = new Mock<IConfigurationSection>();
            sectionMock.Setup(s => s.Value).Returns("true"); // Test Azure Blob Storage saving
            configurationMock.Setup(c => c.GetSection("UseAzureBlobStorage")).Returns(sectionMock.Object);

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

            const string actualContainerName = "images/event-images";

            fileStorageServiceMock.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>(), actualContainerName))
                .ReturnsAsync("https://storageaccount.blob.core.windows.net/events/test.jpg");

            eventRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Event>()))
                .Returns(Task.CompletedTask);

            await createEventsService.CreateEventAsync(model, userId);

            fileStorageServiceMock.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>(), actualContainerName), Times.Once);
            eventRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Event>()), Times.Once);
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