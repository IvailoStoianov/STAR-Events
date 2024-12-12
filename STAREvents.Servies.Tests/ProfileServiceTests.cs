using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using STAREvents.Data.Models;
using STAREvents.Data.Repository;
using STAREvents.Services.Data;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.Data;
using STAREvents.Web.ViewModels.Profile;
using static STAREvents.Common.ErrorMessagesConstants.ProfileServiceErrorMessages;

namespace STAREvents.Services.Tests
{
    [TestFixture]
    public class ProfileServiceTests
    {
        private STAREventsDbContext _context;
        private ProfileService _profileService;
        private Mock<IUserAuthService> _userAuthServiceMock;
        private Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private Mock<IFileStorageService> _fileStorageServiceMock;
        private Mock<IConfiguration> _configurationMock;

        private IList<ApplicationUser> usersData = new List<ApplicationUser>
        {
            new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                UserName = "johndoe",
                isDeleted = false,
                SecurityStamp = Guid.NewGuid().ToString()
            },
            new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                UserName = "janedoe",
                isDeleted = false,
                SecurityStamp = Guid.NewGuid().ToString()
            }
        };

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<STAREventsDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new STAREventsDbContext(options);

            _context.Users.AddRange(usersData);
            _context.SaveChanges();

            _userAuthServiceMock = new Mock<IUserAuthService>();
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _fileStorageServiceMock = new Mock<IFileStorageService>();
            _configurationMock = new Mock<IConfiguration>();

            var sectionMock = new Mock<IConfigurationSection>();
            sectionMock.Setup(s => s.Value).Returns("false"); // Default to local saving
            _configurationMock.Setup(c => c.GetSection("UseAzureBlobStorage")).Returns(sectionMock.Object);

            _profileService = new ProfileService(
                _mockWebHostEnvironment.Object,
                new BaseRepository<Event, object>(_context),
                new BaseRepository<Comment, object>(_context),
                _userAuthServiceMock.Object,
                _fileStorageServiceMock.Object,
                _configurationMock.Object
            );
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            var userId = usersData[0].Id;
            _userAuthServiceMock.Setup(ua => ua.GetUserByIdAsync(userId.ToString())).ReturnsAsync(usersData[0]);

            var result = await _profileService.GetUserByIdAsync(userId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(userId));
        }

        [Test]
        public void GetUserByIdAsync_ShouldThrowKeyNotFoundException_WhenUserDoesNotExist()
        {
            var userId = Guid.NewGuid();
            _userAuthServiceMock.Setup(ua => ua.GetUserByIdAsync(userId.ToString())).ReturnsAsync((ApplicationUser)null);

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _profileService.GetUserByIdAsync(userId)
            );

            Assert.That(ex.Message, Is.EqualTo(UserNotFound));
        }

        [Test]
        public async Task UpdateProfileAsync_WithValidModel_UsesLocalSaving()
        {
            _profileService = new ProfileService(
                _mockWebHostEnvironment.Object,
                new BaseRepository<Event, object>(_context),
                new BaseRepository<Comment, object>(_context),
                _userAuthServiceMock.Object,
                _fileStorageServiceMock.Object,
                new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
                {
            { "UseAzureBlobStorage", "false" }
                }).Build()
            );

            var userId = usersData[0].Id;

            var profileInputModel = new ProfileInputModel
            {
                FirstName = "Smith",
                LastName = "James",
                Email = "Something123@gmail.com",
                Username = "johndoe",
                ProfilePicture = Mock.Of<IFormFile>(x => x.FileName == "test.jpg" && x.Length == 1024),
                ProfilePictureUrl = "/images/default-pfp.svg"
            };

            _userAuthServiceMock.Setup(ua => ua.GetUserByIdAsync(userId.ToString())).ReturnsAsync(usersData[0]);
            _userAuthServiceMock.Setup(ua => ua.UpdateUserAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

            _fileStorageServiceMock.Setup(fs => fs.UploadFileLocallyAsync(It.IsAny<IFormFile>(), "images/profile-pictures"))
                .ReturnsAsync("/images/profile-pictures/test.jpg");

            await _profileService.UpdateProfileAsync(userId, profileInputModel);

            _fileStorageServiceMock.Verify(fs => fs.UploadFileLocallyAsync(It.IsAny<IFormFile>(), "images/profile-pictures"), Times.Once);

            var updatedUser = await _context.Users.FindAsync(userId);

            Assert.That(updatedUser?.FirstName, Is.EqualTo("Smith"));
            Assert.That(updatedUser?.LastName, Is.EqualTo("James"));
            Assert.That(updatedUser?.ProfilePictureUrl, Is.EqualTo("/images/profile-pictures/test.jpg"));
        }

        [Test]
        public async Task UpdateProfileAsync_WithValidModel_UsesAzureBlobStorage()
        {
            _profileService = new ProfileService(
                _mockWebHostEnvironment.Object,
                new BaseRepository<Event, object>(_context),
                new BaseRepository<Comment, object>(_context),
                _userAuthServiceMock.Object,
                _fileStorageServiceMock.Object,
                new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
                {
            { "UseAzureBlobStorage", "true" }
                }).Build()
            );

            var userId = usersData[0].Id;

            var profileInputModel = new ProfileInputModel
            {
                FirstName = "Smith",
                LastName = "James",
                Email = "Something123@gmail.com",
                Username = "johndoe",
                ProfilePicture = Mock.Of<IFormFile>(x => x.FileName == "test.jpg" && x.Length == 1024),
                ProfilePictureUrl = "/images/default-pfp.svg"
            };

            _userAuthServiceMock.Setup(ua => ua.GetUserByIdAsync(userId.ToString())).ReturnsAsync(usersData[0]);
            _userAuthServiceMock.Setup(ua => ua.UpdateUserAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

            _fileStorageServiceMock.Setup(fs => fs.UploadFileAsync(It.IsAny<IFormFile>(), "profile-pictures"))
                .ReturnsAsync("https://storageaccount.blob.core.windows.net/profile-pictures/test.jpg");

            await _profileService.UpdateProfileAsync(userId, profileInputModel);

            _fileStorageServiceMock.Verify(fs => fs.UploadFileAsync(It.IsAny<IFormFile>(), "profile-pictures"), Times.Once);

            var updatedUser = await _context.Users.FindAsync(userId);

            Assert.That(updatedUser?.FirstName, Is.EqualTo("Smith"));
            Assert.That(updatedUser?.LastName, Is.EqualTo("James"));
            Assert.That(updatedUser?.ProfilePictureUrl, Is.EqualTo("https://storageaccount.blob.core.windows.net/profile-pictures/test.jpg"));
        }


        [Test]
        public async Task SoftDeleteProfileAsync_ShouldCallRepositoryDelete_WhenUserExists()
        {
            var userId = usersData[0].Id;
            _userAuthServiceMock.Setup(ua => ua.GetUserByIdAsync(userId.ToString())).ReturnsAsync(usersData[0]);
            _userAuthServiceMock.Setup(ua => ua.UpdateUserAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            _userAuthServiceMock.Setup(ua => ua.LogoutAsync()).Returns(Task.CompletedTask);

            await _profileService.SoftDeleteProfileAsync(userId);

            var deletedUser = await _context.Users.FindAsync(userId);
            Assert.That(deletedUser?.isDeleted, Is.True);
            _userAuthServiceMock.Verify(ua => ua.LogoutAsync(), Times.Once);
        }

        [Test]
        public async Task LoadProfileAsync_ShouldReturnProfileView_WhenUserExists()
        {
            var userId = usersData[0].Id;

            usersData[0].ProfilePictureUrl = "/images/default-pfp.jpg";

            _userAuthServiceMock.Setup(ua => ua.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(usersData[0]);

            var result = await _profileService.LoadProfileAsync(userId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.FirstName, Is.EqualTo("John"));
            Assert.That(result.ProfilePictureUrl, Is.EqualTo("/images/default-pfp.jpg"));
        }

    }
}