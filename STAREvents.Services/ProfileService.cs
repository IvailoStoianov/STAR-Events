using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Profile;
using System.ComponentModel.DataAnnotations;
using static STAREvents.Common.EntityValidationConstants.AllowedExtenstions;
using static STAREvents.Common.ErrorMessagesConstants.ProfileServiceErrorMessages;
using static STAREvents.Common.FilePathConstants.ProfilePicturePaths;

namespace STAREvents.Services.Data
{
    public class ProfileService : BaseService, IProfileService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IRepository<Event, object> eventRepository;
        private readonly IRepository<Comment, object> commentRepository;
        private readonly IUserAuthService userAuthService;

        public ProfileService(
            IWebHostEnvironment webHostEnvironment,
            IRepository<Event, object> eventRepository,
            IRepository<Comment, object> commentRepository,
            IUserAuthService userAuthService)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.eventRepository = eventRepository;
            this.commentRepository = commentRepository;
            this.userAuthService = userAuthService;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
        {
            var user = await userAuthService.GetUserByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }

            return user;
        }

        public async Task<ProfileInputModel> LoadEditFormAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);
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
                    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        profilePicture = new FormFile(fileStream, 0, fileStream.Length, "ProfilePicture", Path.GetFileName(filePath));
                    }
                }
            }

            return new ProfileInputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePictureUrl = user.ProfilePictureUrl,
                ProfilePicture = profilePicture,
                Username = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty
            };
        }


        public async Task<ProfileViewModel> LoadProfileAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);

            return new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePictureUrl = user.ProfilePictureUrl,
                Username = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty
            };
        }

        public async Task UpdateProfileAsync(Guid userId, ProfileInputModel model)
        {
            var user = await GetUserByIdAsync(userId);

            ValidateProfileInputModel(model);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Username;

            if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
            {
                ValidateProfilePicture(model.ProfilePicture);

                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, DefaultProfilePicturePath);
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePicture.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfilePicture.CopyToAsync(fileStream);
                }

                user.ProfilePictureUrl = $"/{DefaultProfilePicturePath}/{uniqueFileName}";
            }

            var result = await userAuthService.UpdateUserAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(FailedToUpdateUserProfile);
            }
        }

        public async Task<IdentityResult> ChangePasswordAsync(Guid userId, ChangePasswordViewModel model)
        {
            var result = await userAuthService.ChangePasswordAsync(userId.ToString(), model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(FailedToChangePassword);
            }

            return result;
        }

        public async Task SoftDeleteProfileAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);
            user.isDeleted = true;

            var userEvents = await eventRepository.GetAllAsync();
            var events = userEvents.Where(e => e.OrganizerID == userId).ToList();
            foreach (var eventEntity in events)
            {
                eventEntity.isDeleted = true;
                await eventRepository.UpdateAsync(eventEntity);
            }

            var userComments = await commentRepository.GetAllAsync();
            var comments = userComments.Where(c => c.UserId == userId).ToList();
            foreach (var comment in comments)
            {
                comment.isDeleted = true;
                await commentRepository.UpdateAsync(comment);
            }

            var result = await userAuthService.UpdateUserAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(FaildSoftDeleteUser);
            }

            await userAuthService.LogoutAsync();
        }

        private void ValidateProfileInputModel(ProfileInputModel model)
        {
            if (string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName) ||
                string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Email))
            {
                throw new ArgumentException(AllFieldsAreRequired);
            }

            if (!new EmailAddressAttribute().IsValid(model.Email))
            {
                throw new ArgumentException(InvalidEmail);
            }
        }

        private void ValidateProfilePicture(IFormFile profilePicture)
        {
            var allowedExtensions = ImageExtensions;
            var extension = Path.GetExtension(profilePicture.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException(InvalidImageFormat);
            }

            var allowedMimeTypes = AllowedMimeTypes;
            if (!allowedMimeTypes.Contains(profilePicture.ContentType))
            {
                throw new InvalidOperationException(InvalidImageFormat);
            }
        }
    }
}
