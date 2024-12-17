using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Profile;
using static STAREvents.Common.ErrorMessagesConstants.ProfileServiceErrorMessages;
using static STAREvents.Common.ModelErrorsConstants.Password;


namespace STAREvents.Services.Tests
{
    [TestFixture]
    public class ProfileServiceTests
    {
        private Mock<IRepository<Event, object>> _eventRepositoryMock;
        private Mock<IRepository<Comment, object>> _commentRepositoryMock;
        private Mock<IUserAuthService> _userAuthServiceMock;
        private Mock<IFileStorageService> _fileStorageServiceMock;
        private Mock<IConfiguration> _configurationMock;
        private ProfileService _profileService;
        private Guid _validUserId;
        private ApplicationUser _validUser;

        [SetUp]
        public void SetUp()
        {
            _eventRepositoryMock = new Mock<IRepository<Event, object>>();
            _commentRepositoryMock = new Mock<IRepository<Comment, object>>();
            _userAuthServiceMock = new Mock<IUserAuthService>();
            _fileStorageServiceMock = new Mock<IFileStorageService>();
            _configurationMock = new Mock<IConfiguration>();

            var sectionMock = new Mock<IConfigurationSection>();
            sectionMock.Setup(s => s.Value).Returns("false"); 
            _configurationMock.Setup(c => c.GetSection("UseAzureBlobStorage")).Returns(sectionMock.Object);

            _validUserId = Guid.NewGuid();
            _validUser = new ApplicationUser
            {
                Id = _validUserId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                UserName = "johndoe",
                ProfilePictureUrl = "/images/default.jpg",
                isDeleted = false
            };

            _userAuthServiceMock.Setup(u => u.GetUserByIdAsync(_validUserId.ToString()))
                .ReturnsAsync(_validUser);

            _profileService = new ProfileService(
                _eventRepositoryMock.Object,
                _commentRepositoryMock.Object,
                _userAuthServiceMock.Object,
                _fileStorageServiceMock.Object,
                _configurationMock.Object
            );
        }


        [Test]
        public async Task GetUserByIdAsync_ReturnsSuccess_WhenUserExists()
        {
            var result = await _profileService.GetUserByIdAsync(_validUserId);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Data, Is.EqualTo(_validUser));
        }
        [Test]
        public async Task GetUserByIdAsync_ReturnsFailure_WhenUserDoesNotExist()
        {
            _userAuthServiceMock.Setup(u => u.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            var result = await _profileService.GetUserByIdAsync(Guid.NewGuid());

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(UserNotFound));
        }
        [Test]
        public async Task UpdateProfileAsync_ReturnsSuccess_WhenValidDataProvided()
        {
            var model = new ProfileInputModel
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Username = "janesmith",
                ProfilePicture = null
            };

            _userAuthServiceMock.Setup(u => u.UpdateUserAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _profileService.UpdateProfileAsync(_validUserId, model);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(_validUser.FirstName, Is.EqualTo("Jane"));
            Assert.That(_validUser.Email, Is.EqualTo("jane.smith@example.com"));
        }
        [Test]
        public async Task UpdateProfileAsync_ReturnsFailure_WhenInvalidEmailProvided()
        {
            var model = new ProfileInputModel
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "invalid-email",
                Username = "janesmith"
            };

            var result = await _profileService.UpdateProfileAsync(_validUserId, model);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(InvalidEmail));
        }
        [Test]
        public async Task SoftDeleteProfileAsync_ReturnsSuccess_WhenUserDeleted()
        {
            _userAuthServiceMock.Setup(u => u.UpdateUserAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);
            _userAuthServiceMock.Setup(u => u.LogoutAsync()).Returns(Task.CompletedTask);

            var result = await _profileService.SoftDeleteProfileAsync(_validUserId);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(_validUser.isDeleted, Is.True);
        }
        [Test]
        public async Task SoftDeleteProfileAsync_ReturnsFailure_WhenUpdateFails()
        {
            _userAuthServiceMock.Setup(u => u.UpdateUserAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Update failed" }));

            var result = await _profileService.SoftDeleteProfileAsync(_validUserId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(FaildSoftDeleteUser));
        }
        [Test]
        public async Task ChangePasswordAsync_ReturnsSuccess_WhenPasswordChanged()
        {
            var model = new ChangePasswordViewModel
            {
                CurrentPassword = "OldPass123!",
                NewPassword = "NewPass123!",
                ConfirmPassword = "NewPass123!"
            };

            _userAuthServiceMock.Setup(u => u.ChangePasswordAsync(
                _validUserId.ToString(), model.CurrentPassword, model.NewPassword))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _profileService.ChangePasswordAsync(_validUserId, model);

            Assert.That(result.Succeeded, Is.True);
        }
        [Test]
        public async Task ChangePasswordAsync_ReturnsFailure_WhenPasswordsDoNotMatch()
        {
            var model = new ChangePasswordViewModel
            {
                CurrentPassword = "OldPass123!",
                NewPassword = "NewPass123!",
                ConfirmPassword = "MismatchPass123!"
            };

            var result = await _profileService.ChangePasswordAsync(_validUserId, model);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(PasswordsDontMatch));
        }
        [Test]
        public async Task UpdateProfileAsync_ReturnsFailure_WhenRequiredFieldsAreEmpty()
        {
            var model = new ProfileInputModel
            {
                FirstName = "",
                LastName = "",
                Email = "",
                Username = ""
            };

            var result = await _profileService.UpdateProfileAsync(_validUserId, model);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(AllFieldsAreRequired));
        }

        [Test]
        public async Task UpdateProfileAsync_ReturnsFailure_WhenProfilePictureInvalidFormat()
        {
            var model = new ProfileInputModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Username = "johndoe",
                ProfilePicture = Mock.Of<IFormFile>(f => f.FileName == "invalid.exe")
            };

            var result = await _profileService.UpdateProfileAsync(_validUserId, model);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(InvalidImageFormat));
        }

        [Test]
        public async Task UpdateProfileAsync_ReturnsSuccess_WhenProfilePictureUploadedLocally()
        {
            var model = new ProfileInputModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Username = "johndoe",
                ProfilePicture = Mock.Of<IFormFile>(f => f.FileName == "profile.jpg")
            };

            _fileStorageServiceMock.Setup(f => f.UploadFileLocallyAsync(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync("/images/profile-pictures/profile.jpg");

            _userAuthServiceMock.Setup(u => u.UpdateUserAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _profileService.UpdateProfileAsync(_validUserId, model);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(_validUser.ProfilePictureUrl, Is.EqualTo("/images/profile-pictures/profile.jpg"));
        }

        [Test]
        public async Task UpdateProfileAsync_ReturnsSuccess_WhenProfilePictureUploadedToAzure()
        {
            var model = new ProfileInputModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Username = "johndoe",
                ProfilePicture = Mock.Of<IFormFile>(f => f.FileName == "profile.jpg")
            };

            var sectionMock = new Mock<IConfigurationSection>();
            sectionMock.Setup(s => s.Value).Returns("true");
            _configurationMock.Setup(c => c.GetSection("UseAzureBlobStorage")).Returns(sectionMock.Object);

            _fileStorageServiceMock.Setup(f => f.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync("https://storageaccount.blob.core.windows.net/profile-pictures/profile.jpg");

            _userAuthServiceMock.Setup(u => u.UpdateUserAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _profileService.UpdateProfileAsync(_validUserId, model);

            Assert.That(result.Succeeded, Is.True);
        }

        [Test]
        public async Task SoftDeleteProfileAsync_DeletesEventsAndComments()
        {
            var events = new List<Event>
                {
                    new Event { EventId = Guid.NewGuid(), OrganizerID = _validUserId, isDeleted = false }
                };

                        var comments = new List<Comment>
                {
                    new Comment { CommentId = Guid.NewGuid(), UserId = _validUserId, isDeleted = false }
                };

            _eventRepositoryMock.Setup(e => e.GetAllAsync()).ReturnsAsync(events);
            _commentRepositoryMock.Setup(c => c.GetAllAsync()).ReturnsAsync(comments);
            _userAuthServiceMock.Setup(u => u.UpdateUserAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);
            _userAuthServiceMock.Setup(u => u.LogoutAsync()).Returns(Task.CompletedTask);

            var result = await _profileService.SoftDeleteProfileAsync(_validUserId);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(events[0].isDeleted, Is.True);
            Assert.That(comments[0].isDeleted, Is.True);
            _eventRepositoryMock.Verify(e => e.UpdateAsync(events[0]), Times.Once);
            _commentRepositoryMock.Verify(c => c.UpdateAsync(comments[0]), Times.Once);
        }
        [Test]
        public async Task LoadEditFormAsync_ReturnsFailure_WhenUserNotFound()
        {
            _userAuthServiceMock.Setup(u => u.GetUserByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            var result = await _profileService.LoadEditFormAsync(_validUserId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(UserNotFound));
        }

        [Test]
        public async Task LoadProfileAsync_ReturnsFailure_WhenUserNotFound()
        {
            _userAuthServiceMock.Setup(u => u.GetUserByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            var result = await _profileService.LoadProfileAsync(_validUserId);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Contains.Item(UserNotFound));
        }

        [Test]
        public async Task LoadEditFormAsync_ReturnsCorrectModel_WhenUserExists()
        {
            var result = await _profileService.LoadEditFormAsync(_validUserId);

            Assert.That(result.Succeeded, Is.True);
            var data = result.Data;
            Assert.That(data.FirstName, Is.EqualTo(_validUser.FirstName));
            Assert.That(data.LastName, Is.EqualTo(_validUser.LastName));
            Assert.That(data.Email, Is.EqualTo(_validUser.Email));
            Assert.That(data.Username, Is.EqualTo(_validUser.UserName));
            Assert.That(data.ProfilePictureUrl, Is.EqualTo(_validUser.ProfilePictureUrl));
        }

        [Test]
        public async Task LoadProfileAsync_ReturnsCorrectModel_WhenUserExists()
        {
            var result = await _profileService.LoadProfileAsync(_validUserId);

            Assert.That(result.Succeeded, Is.True);
            var data = result.Data;
            Assert.That(data.FirstName, Is.EqualTo(_validUser.FirstName));
            Assert.That(data.LastName, Is.EqualTo(_validUser.LastName));
            Assert.That(data.Email, Is.EqualTo(_validUser.Email));
            Assert.That(data.Username, Is.EqualTo(_validUser.UserName));
            Assert.That(data.ProfilePictureUrl, Is.EqualTo(_validUser.ProfilePictureUrl));
        }


    }
}
