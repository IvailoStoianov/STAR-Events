using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

            _profileService = new ProfileService(
                _mockWebHostEnvironment.Object,
                new BaseRepository<Event, object>(_context),
                new BaseRepository<Comment, object>(_context),
                _userAuthServiceMock.Object
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
        public async Task UpdateProfileAsync_ShouldCallRepositoryUpdate_WhenProfileIsValid()
        {
            var userId = usersData[0].Id;
            var profileInputModel = new ProfileInputModel
            {
                FirstName = "Smith",
                LastName = "James",
                Email = "Something123@gmail.com",
                Username = "johndoe",
                ProfilePicture = null,
                ProfilePictureUrl = "/images/default-pfp.svg"
            };
            _userAuthServiceMock.Setup(ua => ua.GetUserByIdAsync(userId.ToString())).ReturnsAsync(usersData[0]);
            _userAuthServiceMock.Setup(ua => ua.UpdateUserAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

            await _profileService.UpdateProfileAsync(userId, profileInputModel);

            var updatedUser = await _context.Users.FindAsync(userId);
            Assert.That(updatedUser?.FirstName, Is.EqualTo("Smith"));
            Assert.That(updatedUser?.LastName, Is.EqualTo("James"));
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
        public async Task LoadEditFormAsync_ShouldReturnProfile_WhenUserExists()
        {
            var userId = usersData[0].Id;
            var expectedProfilePicturePath = Path.Combine("wwwroot", "images", "default-pfp.svg");

            _userAuthServiceMock.Setup(ua => ua.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(usersData[0]);

            _mockWebHostEnvironment.Setup(whe => whe.WebRootPath)
                .Returns("wwwroot");

            File.WriteAllText(expectedProfilePicturePath, "Dummy content");

            var result = await _profileService.LoadEditFormAsync(userId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.FirstName, Is.EqualTo("John"));
            Assert.That(result.LastName, Is.EqualTo("Doe"));
            Assert.That(result.Username, Is.EqualTo("johndoe"));
            Assert.That(result.Email, Is.EqualTo("john.doe@example.com"));
            Assert.That(result.ProfilePictureUrl, Is.EqualTo("/images/default-pfp.svg"));
            Assert.That(result.ProfilePicture, Is.Not.Null);
            Assert.That(result.ProfilePicture.FileName, Is.EqualTo("default-pfp.svg"));

            File.Delete(expectedProfilePicturePath);
        }

        [Test]
        public async Task LoadProfileAsync_ShouldReturnProfileView_WhenUserExists()
        {
            var userId = usersData[0].Id;
            _userAuthServiceMock.Setup(ua => ua.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(usersData[0]);

            var result = await _profileService.LoadProfileAsync(userId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.FirstName, Is.EqualTo("John"));
            Assert.That(result.ProfilePictureUrl, Is.EqualTo("/images/default-pfp.svg"));
        }

        [Test]
        public void UpdateProfileAsync_ShouldThrowArgumentException_WhenModelIsInvalid()
        {
            var userId = usersData[0].Id;
            var invalidModel = new ProfileInputModel
            {
                FirstName = "",
                LastName = "",
                Email = "invalid-email",
                Username = ""
            };

            _userAuthServiceMock.Setup(ua => ua.GetUserByIdAsync(userId.ToString()))
            .ReturnsAsync(usersData[0]);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await _profileService.UpdateProfileAsync(userId, invalidModel)
            );

            Assert.That(ex.Message, Is.EqualTo(AllFieldsAreRequired));
        }

        [Test]
        public async Task UpdateProfileAsync_ShouldThrowInvalidOperationException_WhenUpdateFails()
        {
            var userId = usersData[0].Id;
            var validModel = new ProfileInputModel
            {
                FirstName = "Updated",
                LastName = "Name",
                Email = "updated@example.com",
                Username = "updateduser"
            };

            _userAuthServiceMock.Setup(ua => ua.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(usersData[0]);
            _userAuthServiceMock.Setup(ua => ua.UpdateUserAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Update failed" }));

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _profileService.UpdateProfileAsync(userId, validModel)
            );

            Assert.That(ex.Message, Is.EqualTo(FailedToUpdateUserProfile));
        }

        [Test]
        public async Task ChangePasswordAsync_ShouldThrowInvalidOperationException_WhenChangeFails()
        {
            var userId = usersData[0].Id;
            var changePasswordModel = new ChangePasswordViewModel
            {
                CurrentPassword = "OldPassword",
                NewPassword = "NewPassword123"
            };

            _userAuthServiceMock.Setup(ua => ua.ChangePasswordAsync(userId.ToString(), "OldPassword", "NewPassword123"))
                .ReturnsAsync(IdentityResult.Failed());

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _profileService.ChangePasswordAsync(userId, changePasswordModel)
            );

            Assert.That(ex.Message, Is.EqualTo("Failed to change password."));
        }

        [Test]
        public async Task SoftDeleteProfileAsync_ShouldThrowInvalidOperationException_WhenUpdateFails()
        {
            var userId = usersData[0].Id;
            _userAuthServiceMock.Setup(ua => ua.GetUserByIdAsync(userId.ToString()))
                .ReturnsAsync(usersData[0]);
            _userAuthServiceMock.Setup(ua => ua.UpdateUserAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Failed());

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _profileService.SoftDeleteProfileAsync(userId)
            );

            Assert.That(ex.Message, Is.EqualTo("Failed to soft delete user."));
        }
    }
}
