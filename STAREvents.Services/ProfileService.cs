using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Profile;
using static STAREvents.Common.ErrorMessagesConstants.ProfileServiceErrorMessages;
using static STAREvents.Common.EntityValidationConstants.ApplicationUserConstants;

namespace STAREvents.Services.Data
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public ProfileService(UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = _userManager;
            this.signInManager = signInManager;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
        {
            var user = await userManager.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }

            return user;
        }

        public async Task<ProfileInputModel> LoadEditFormAsync(Guid userId)
        {
            var user = await userManager.Users
                .Where(u => u.Id == userId)
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.ProfilePicture,
                    u.UserName,
                    u.Email
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }

            var profileInputModel = new ProfileInputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePicture = user.ProfilePicture,
                Username = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty
            };

            return profileInputModel;
        }

        public async Task<ProfileViewModel> LoadProfileAsync(Guid userId)
        {
            var userProfile = await userManager.Users
                .Where(u => u.Id == userId)
                .Select(u => new ProfileViewModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    ProfilePicture = u.ProfilePicture,
                    Username = u.UserName ?? string.Empty,
                    Email = u.Email ?? string.Empty
                })
                .FirstOrDefaultAsync();

            if (userProfile == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }

            return userProfile;
        }

        public async Task UpdateProfileAsync(Guid userId, ProfileInputModel model)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }

            // Validate profile picture size
            if (model.ProfilePicture != null && model.ProfilePicture.Length > MaxProfilePictureSize)
            {
                throw new InvalidOperationException(string.Format(ProfilePictureSizeExceeded, MaxProfilePictureSize));
            }

            // Update user properties if they are not equal to null or empty
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Username;

            // Update profile picture if provided else save the old one to the model so upon update it is not lost
            if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
            {
                user.ProfilePicture = model.ProfilePicture;
            }
            else
            {
                model.ProfilePicture = user.ProfilePicture;
            }

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(FailedToUpdateUserProfile);
            }
        }
        public async Task<IdentityResult> ChangePasswordAsync(Guid userId, ChangePasswordViewModel model)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }

            if (string.IsNullOrEmpty(model.CurrentPassword) || string.IsNullOrEmpty(model.NewPassword))
            {
                throw new ArgumentException(PasswordsAreRequired);
            }

            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                await signInManager.RefreshSignInAsync(user);
            }

            return result;
        }
    }
}

