using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Profile;
using static STAREvents.Common.EntityValidationConstants.AllowedExtenstions;
using static STAREvents.Common.ErrorMessagesConstants.ProfileServiceErrorMessages;
using static STAREvents.Common.FilePathConstants.ProfilePicturePaths;

namespace STAREvents.Services.Data
{
    public class ProfileService : BaseService, IProfileService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProfileService(UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = _userManager;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
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
                    u.ProfilePictureUrl,
                    u.UserName,
                    u.Email
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }

            IFormFile profilePicture = null;
            if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
            {
                var filePath = Path.Combine(webHostEnvironment.WebRootPath, user.ProfilePictureUrl.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    profilePicture = new FormFile(fileStream, 0, fileStream.Length, "ProfilePicture", Path.GetFileName(filePath));
                }
            }

            var profileInputModel = new ProfileInputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePictureUrl = user.ProfilePictureUrl,
                ProfilePicture = profilePicture,
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
                    ProfilePictureUrl = u.ProfilePictureUrl,
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

            // Update user properties if they are not equal to null or empty
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Username;

            // Update profile picture if provided
            if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
            {
                // Validate file type
                var allowedExtensions = ImageExtensions;
                var extension = Path.GetExtension(model.ProfilePicture.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException(InvalidImageFormat);
                }

                // Validate MIME type
                var allowedMimeTypes = AllowedMimeTypes;
                if (!allowedMimeTypes.Contains(model.ProfilePicture.ContentType))
                {
                    throw new InvalidOperationException(InvalidImageFormat);
                }

                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, DefaultProfilePicturePath);
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePicture.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfilePicture.CopyToAsync(fileStream);
                }

                user.ProfilePictureUrl = $"/{DefaultProfilePicturePath}/{uniqueFileName}";
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


