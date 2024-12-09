using Microsoft.AspNetCore.Identity;
using STAREvents.Data.Models;
using STAREvents.Web.ViewModels.Profile;


namespace STAREvents.Services.Data.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileViewModel> LoadProfileAsync(Guid userId);
        Task<ProfileInputModel> LoadEditFormAsync(Guid userId);
        Task<ApplicationUser> GetUserByIdAsync(Guid userId);
        Task UpdateProfileAsync(Guid userId, ProfileInputModel model);
        Task<IdentityResult> ChangePasswordAsync(Guid userId, ChangePasswordViewModel model);
        Task SoftDeleteProfileAsync(Guid userId);
    }
}
