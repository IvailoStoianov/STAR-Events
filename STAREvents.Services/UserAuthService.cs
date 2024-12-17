using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using STAREvents.Data.Models;
using STAREvents.Services.Data.Interfaces;

using static STAREvents.Common.ErrorMessagesConstants.UserAuthServiceMessages;
using static STAREvents.Common.ErrorMessagesConstants.SharedErrorMessages;

namespace STAREvents.Services.Data
{
    public class UserAuthService : BaseService, IUserAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserAuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<SignInResult> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return SignInResult.Failed;
            }

            return await _signInManager.PasswordSignInAsync(user, password, false, false);
        }

        public async Task<IdentityResult> RegisterAsync(string username, string password, string email)
        {
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email
            };
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = UserNotFound });
            }

            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }
        public async Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = UserNotFound });
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result;
        }
        public async Task<IdentityResult> AddRoleToUserAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = UserNotFound });
            }

            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                return await _userManager.AddToRoleAsync(user, roleName);
            }

            return IdentityResult.Failed(new IdentityError { Description = UserAlreadyInRole });
        }

        public async Task<IdentityResult> RemoveRoleFromUserAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = UserNotFound });
            }

            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                return await _userManager.RemoveFromRoleAsync(user, roleName);
            }

            return IdentityResult.Failed(new IdentityError { Description = UserIsNotInRole });
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new List<string>();
            }

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }
        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser?> GetUserByNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user;
        }

        public async Task<bool> IsUserInRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null && await _userManager.IsInRoleAsync(user, roleName);
        }
    }
}
