using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Moq;

using STAREvents.Data.Models;
using STAREvents.Services.Data;

using static STAREvents.Common.EntityValidationConstants.RoleNames;
using static STAREvents.Common.ErrorMessagesConstants.UserAuthServiceMessages;
using static STAREvents.Common.ErrorMessagesConstants.SharedErrorMessages;

namespace STAREvents.Services.Tests
{


    [TestFixture]
    public class UserAuthServiceTests
    {
        private Mock<UserManager<ApplicationUser>> userManagerMock;
        private Mock<SignInManager<ApplicationUser>> signInManagerMock;
        private UserAuthService userAuthService;

        [SetUp]
        public void SetUp()
        {
            userManagerMock = MockUserManager();
            signInManagerMock = MockSignInManager();

            userAuthService = new UserAuthService(userManagerMock.Object, signInManagerMock.Object);
        }

        [Test]
        public async Task LoginAsync_UserNotFound_ReturnsSignInFailed()
        {
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser?)null);

            var result = await userAuthService.LoginAsync("testuser", "password");

            Assert.That(result, Is.EqualTo(SignInResult.Failed));
        }

        [Test]
        public async Task LoginAsync_ValidUser_ReturnsSignInSuccess()
        {
            var user = new ApplicationUser { UserName = "testuser" };

            userManagerMock.Setup(x => x.FindByNameAsync("testuser"))
                .ReturnsAsync(user);
            signInManagerMock.Setup(x => x.PasswordSignInAsync(user, "password", false, false))
                .ReturnsAsync(SignInResult.Success);

            var result = await userAuthService.LoginAsync("testuser", "password");

            Assert.That(result, Is.EqualTo(SignInResult.Success));
        }

        [Test]
        public async Task RegisterAsync_ValidUser_ReturnsSuccess()
        {
            var user = new ApplicationUser { UserName = "testuser", Email = "test@test.com" };

            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), "password"))
                .ReturnsAsync(IdentityResult.Success);

            var result = await userAuthService.RegisterAsync("testuser", "password", "test@test.com");

            Assert.That(result, Is.EqualTo(IdentityResult.Success));
        }

        [Test]
        public async Task LogoutAsync_CallsSignOut()
        {
            await userAuthService.LogoutAsync();

            signInManagerMock.Verify(x => x.SignOutAsync(), Times.Once);
        }

        [Test]
        public async Task ResetPasswordAsync_UserNotFound_ReturnsFailed()
        {
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser?)null);

            var result = await userAuthService.ResetPasswordAsync("userId", "token", "newPassword");

            Assert.That(result.Succeeded, Is.False);
        }

        [Test]
        public async Task ResetPasswordAsync_ValidUser_ReturnsSuccess()
        {
            var user = new ApplicationUser();

            userManagerMock.Setup(x => x.FindByIdAsync("userId"))
                .ReturnsAsync(user);
            userManagerMock.Setup(x => x.ResetPasswordAsync(user, "token", "newPassword"))
                .ReturnsAsync(IdentityResult.Success);

            var result = await userAuthService.ResetPasswordAsync("userId", "token", "newPassword");

            Assert.That(result.Succeeded, Is.True);
        }

        [Test]
        public async Task ChangePasswordAsync_UserNotFound_ReturnsFailed()
        {
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser?)null);

            var result = await userAuthService.ChangePasswordAsync("userId", "currentPassword", "newPassword");

            Assert.That(result.Succeeded, Is.False);
        }

        [Test]
        public async Task ChangePasswordAsync_ValidUser_ReturnsSuccess()
        {
            var user = new ApplicationUser();

            userManagerMock.Setup(x => x.FindByIdAsync("userId"))
                .ReturnsAsync(user);
            userManagerMock.Setup(x => x.ChangePasswordAsync(user, "currentPassword", "newPassword"))
                .ReturnsAsync(IdentityResult.Success);

            var result = await userAuthService.ChangePasswordAsync("userId", "currentPassword", "newPassword");

            Assert.That(result.Succeeded, Is.True);
        }

        [Test]
        public async Task AddRoleToUserAsync_ValidUser_ReturnsSuccess()
        {
            var user = new ApplicationUser();

            userManagerMock.Setup(x => x.FindByIdAsync("userId"))
                .ReturnsAsync(user);
            userManagerMock.Setup(x => x.AddToRoleAsync(user, "role"))
                .ReturnsAsync(IdentityResult.Success);

            var result = await userAuthService.AddRoleToUserAsync("userId", "role");

            Assert.That(result.Succeeded, Is.True);
        }

        [Test]
        public async Task GetUserRolesAsync_ValidUser_ReturnsRoles()
        {
            var user = new ApplicationUser();

            userManagerMock.Setup(x => x.FindByIdAsync("userId"))
                .ReturnsAsync(user);
            userManagerMock.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { Administrator, User });

            var roles = await userAuthService.GetUserRolesAsync("userId");

            Assert.That(roles, Has.Count.EqualTo(2));
            Assert.That(roles, Contains.Item(Administrator));
            Assert.That(roles, Contains.Item(User));
        }

        [Test]
        public async Task GetUserByIdAsync_UserFound_ReturnsUser()
        {
            var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "testuser" };

            userManagerMock.Setup(x => x.FindByIdAsync("userId"))
                .ReturnsAsync(user);

            var result = await userAuthService.GetUserByIdAsync("userId");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserName, Is.EqualTo("testuser"));
        }
        [Test]
        public async Task UpdateUserAsync_ReturnsSuccess_WhenUserUpdated()
        {
            var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "testuser" };

            userManagerMock.Setup(x => x.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

            var result = await userAuthService.UpdateUserAsync(user);

            Assert.That(result.Succeeded, Is.True);
            userManagerMock.Verify(x => x.UpdateAsync(user), Times.Once);
        }

        [Test]
        public async Task UpdateUserAsync_ReturnsFailure_WhenUpdateFails()
        {
            var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "testuser" };

            userManagerMock.Setup(x => x.UpdateAsync(user))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Update failed" }));

            var result = await userAuthService.UpdateUserAsync(user);

            Assert.That(result.Succeeded, Is.False);
        }



        [Test]
        public async Task GetUserByNameAsync_ReturnsUser_WhenUserExists()
        {
            var user = new ApplicationUser { UserName = "testuser" };

            userManagerMock.Setup(x => x.FindByNameAsync("testuser")).ReturnsAsync(user);

            var result = await userAuthService.GetUserByNameAsync("testuser");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserName, Is.EqualTo("testuser"));
        }

        [Test]
        public async Task GetUserByNameAsync_ReturnsNull_WhenUserNotFound()
        {
            userManagerMock.Setup(x => x.FindByNameAsync("testuser")).ReturnsAsync((ApplicationUser?)null);

            var result = await userAuthService.GetUserByNameAsync("testuser");

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task IsUserInRoleAsync_ReturnsTrue_WhenUserInRole()
        {
            var user = new ApplicationUser { Id = Guid.NewGuid() };

            userManagerMock.Setup(x => x.FindByIdAsync(user.Id.ToString())).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

            var result = await userAuthService.IsUserInRoleAsync(user.Id.ToString(), "Admin");

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IsUserInRoleAsync_ReturnsFalse_WhenUserNotInRole()
        {
            var user = new ApplicationUser { Id = Guid.NewGuid() };

            userManagerMock.Setup(x => x.FindByIdAsync(user.Id.ToString())).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

            var result = await userAuthService.IsUserInRoleAsync(user.Id.ToString(), "Admin");

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RemoveRoleFromUserAsync_ReturnsSuccess_WhenRoleRemoved()
        {
            var user = new ApplicationUser { Id = Guid.NewGuid() };

            userManagerMock.Setup(x => x.FindByIdAsync(user.Id.ToString())).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);
            userManagerMock.Setup(x => x.RemoveFromRoleAsync(user, "Admin")).ReturnsAsync(IdentityResult.Success);

            var result = await userAuthService.RemoveRoleFromUserAsync(user.Id.ToString(), "Admin");

            Assert.That(result.Succeeded, Is.True);
        }

        [Test]
        public async Task RemoveRoleFromUserAsync_ReturnsFailure_WhenUserNotFound()
        {
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser?)null);

            var result = await userAuthService.RemoveRoleFromUserAsync("invalidId", "Admin");

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors.First().Description, Is.EqualTo(UserNotFound));
        }

        [Test]
        public async Task RemoveRoleFromUserAsync_ReturnsFailure_WhenUserNotInRole()
        {
            var user = new ApplicationUser { Id = Guid.NewGuid() };

            userManagerMock.Setup(x => x.FindByIdAsync(user.Id.ToString())).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

            var result = await userAuthService.RemoveRoleFromUserAsync(user.Id.ToString(), "Admin");

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors.First().Description, Is.EqualTo(UserIsNotInRole));
        }
        private static Mock<UserManager<ApplicationUser>> MockUserManager()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            return new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        private static Mock<SignInManager<ApplicationUser>> MockSignInManager()
        {
            var userManager = MockUserManager();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            return new Mock<SignInManager<ApplicationUser>>(userManager.Object, contextAccessor.Object, claimsFactory.Object, null, null, null, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }
    }

}
