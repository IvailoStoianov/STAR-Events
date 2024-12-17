using Microsoft.Extensions.Configuration;
using STAREvents.Common;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Profile;
using System.ComponentModel.DataAnnotations;
using static STAREvents.Common.EntityValidationConstants.AllowedExtenstions;
using static STAREvents.Common.ErrorMessagesConstants.ProfileServiceErrorMessages;
using static STAREvents.Common.ErrorMessagesConstants.SharedErrorMessages;
using static STAREvents.Common.FilePathConstants.AzureContainerNames;
using static STAREvents.Common.FilePathConstants.ProfilePicturePaths;
using static STAREvents.Common.ModelErrorsConstants.Password;

namespace STAREvents.Services.Data
{
    public class ProfileService : BaseService, IProfileService
    {
        private readonly IRepository<Event, object> eventRepository;
        private readonly IRepository<Comment, object> commentRepository;
        private readonly IUserAuthService userAuthService;
        private readonly IFileStorageService fileStorageService;
        private readonly bool useAzureBlobStorage;

        public ProfileService(
            IRepository<Event, object> eventRepository,
            IRepository<Comment, object> commentRepository,
            IUserAuthService userAuthService,
            IFileStorageService fileStorageService,
            IConfiguration configuration)
        {
            this.eventRepository = eventRepository;
            this.commentRepository = commentRepository;
            this.userAuthService = userAuthService;
            this.fileStorageService = fileStorageService;

            useAzureBlobStorage = configuration.GetValue<bool>("UseAzureBlobStorage");
        }

        public async Task<ServiceResult<ApplicationUser>> GetUserByIdAsync(Guid userId)
        {
            var user = await userAuthService.GetUserByIdAsync(userId.ToString());
            if (user == null)
                return ServiceResult<ApplicationUser>.Failure(UserNotFound);

            return ServiceResult<ApplicationUser>.Success(user);
        }

        public async Task<ServiceResult<ProfileInputModel>> LoadEditFormAsync(Guid userId)
        {
            var userResult = await GetUserByIdAsync(userId);
            if (!userResult.Succeeded || userResult.Data == null)
                return ServiceResult<ProfileInputModel>.Failure(userResult.Errors);

            var user = userResult.Data;
            return ServiceResult<ProfileInputModel>.Success(new ProfileInputModel
            {
                FirstName = user?.FirstName ?? string.Empty,
                LastName = user?.LastName ?? string.Empty,
                ProfilePictureUrl = user?.ProfilePictureUrl ?? string.Empty,
                Username = user?.UserName ?? string.Empty,
                Email = user?.Email ?? string.Empty
            });
        }

        public async Task<ServiceResult<ProfileViewModel>> LoadProfileAsync(Guid userId)
        {
            var userResult = await GetUserByIdAsync(userId);
            if (!userResult.Succeeded || userResult.Data == null)
                return ServiceResult<ProfileViewModel>.Failure(userResult.Errors);

            var user = userResult.Data;
            return ServiceResult<ProfileViewModel>.Success(new ProfileViewModel
            {
                FirstName = user?.FirstName ?? string.Empty,
                LastName = user?.LastName ?? string.Empty,
                ProfilePictureUrl = user?.ProfilePictureUrl ?? string.Empty,
                Username = user?.UserName ?? string.Empty,
                Email = user?.Email ?? string.Empty
            });
        }

        public async Task<ServiceResult> UpdateProfileAsync(Guid userId, ProfileInputModel model)
        {
            var userResult = await GetUserByIdAsync(userId);
            if (!userResult.Succeeded)
                return ServiceResult.Failure(userResult.Errors);

            var user = userResult.Data;

            if (string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName) ||
                string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Email))
            {
                return ServiceResult.Failure(AllFieldsAreRequired);
            }

            if (!new EmailAddressAttribute().IsValid(model.Email))
            {
                return ServiceResult.Failure(InvalidEmail);
            }

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.Username;

                if (model.ProfilePicture != null)
                {
                    var extension = Path.GetExtension(model.ProfilePicture.FileName).ToLowerInvariant();
                    if (!ImageExtensions.Contains(extension))
                    {
                        return ServiceResult.Failure(InvalidImageFormat);
                    }

                    string profilePictureUrl;
                    if (useAzureBlobStorage)
                    {
                        profilePictureUrl = await fileStorageService.UploadFileAsync(model.ProfilePicture, ProfilePicturesContainer);
                    }
                    else
                    {
                        profilePictureUrl = await fileStorageService.UploadFileLocallyAsync(model.ProfilePicture, DefaultProfilePicturePath);
                    }

                    user.ProfilePictureUrl = profilePictureUrl;
                }

                var result = await userAuthService.UpdateUserAsync(user);
                return result.Succeeded ? ServiceResult.Success() : ServiceResult.Failure(FailedToUpdateUserProfile);
            }

            return ServiceResult.Failure(UserNotFound);
        }

        public async Task<ServiceResult> ChangePasswordAsync(Guid userId, ChangePasswordViewModel model)
        {
            if (model.NewPassword != model.ConfirmPassword)
                return ServiceResult.Failure(PasswordsDontMatch);

            if (string.IsNullOrEmpty(model.CurrentPassword) || string.IsNullOrEmpty(model.NewPassword))
                return ServiceResult.Failure(AllFieldsAreRequired);

            var result = await userAuthService.ChangePasswordAsync(userId.ToString(), model.CurrentPassword, model.NewPassword);

            return result.Succeeded
                ? ServiceResult.Success()
                : ServiceResult.Failure(result.Errors.Select(e => e.Description).ToList());
        }

        public async Task<ServiceResult> SoftDeleteProfileAsync(Guid userId)
        {
            var userResult = await GetUserByIdAsync(userId);
            if (!userResult.Succeeded || userResult.Data == null)
                return ServiceResult.Failure(userResult.Errors);

            var user = userResult.Data;
            user.isDeleted = true;

            var userEvents = await eventRepository.GetAllAsync();
            foreach (var e in userEvents.Where(e => e.OrganizerID == userId))
            {
                e.isDeleted = true;
                await eventRepository.UpdateAsync(e);
            }

            var userComments = await commentRepository.GetAllAsync();
            foreach (var c in userComments.Where(c => c.UserId == userId))
            {
                c.isDeleted = true;
                await commentRepository.UpdateAsync(c);
            }

            var result = await userAuthService.UpdateUserAsync(user);
            if (!result.Succeeded)
                return ServiceResult.Failure(FaildSoftDeleteUser);

            await userAuthService.LogoutAsync();
            return ServiceResult.Success();
        }
        

    }
}