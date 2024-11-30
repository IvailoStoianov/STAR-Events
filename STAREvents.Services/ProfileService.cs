using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Services.Mapping;
using STAREvents.Web.ViewModels.Profile;

using static STAREvents.Common.ErrorMessagesConstants.ProfileServiceErrorMessages;

namespace STAREvents.Services.Data
{
    public class ProfileService : BaseService, IProfileService
    {
        private readonly UserManager<ApplicationUser> userManager;
        public ProfileService(UserManager<ApplicationUser> _userManager)
        {
            this.userManager = _userManager;
        }

        public async Task<ProfileInputModel> LoadEditFormAsync(Guid userId)
        {
            var user = await userManager.Users
                .Where(u => u.Id == userId)
                .To<ProfileInputModel>()
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }

            return user;
        }

        public async Task<ProfileViewModel> LoadProfileAsync(Guid userId)
        {
            ProfileViewModel? userProfile = await userManager.Users
                .Where(u => u.Id == userId)
                .To<ProfileViewModel>()
                .FirstOrDefaultAsync();


            if (userProfile == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }

            return userProfile;

        }

    }
}
