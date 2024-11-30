using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Services.Mapping;
using STAREvents.Web.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Services.Data
{
    public class ProfileService : BaseService, IProfileService
    {
        private readonly UserManager<ApplicationUser> userManager;
        public ProfileService(UserManager<ApplicationUser> _userManager)
        {
            this.userManager = _userManager;
        }

        public async Task<ProfileViewModel> LoadProfileAsync(Guid userId)
        {
            ProfileViewModel? userProfile = await userManager.Users
                .Where(u => u.Id == userId)
                .To<ProfileViewModel>()
                .FirstOrDefaultAsync();


            if (userProfile == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            return userProfile;

        }

    }
}
