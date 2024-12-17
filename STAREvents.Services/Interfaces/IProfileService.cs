using STAREvents.Common;
using STAREvents.Data.Models;
using STAREvents.Web.ViewModels.Profile;

namespace STAREvents.Services.Data.Interfaces
{
    public interface IProfileService
    {
        Task<ServiceResult<ProfileViewModel>> LoadProfileAsync(Guid userId);
        Task<ServiceResult<ProfileInputModel>> LoadEditFormAsync(Guid userId);
        Task<ServiceResult<ApplicationUser>> GetUserByIdAsync(Guid userId);
        Task<ServiceResult> UpdateProfileAsync(Guid userId, ProfileInputModel model);
        Task<ServiceResult> ChangePasswordAsync(Guid userId, ChangePasswordViewModel model);
        Task<ServiceResult> SoftDeleteProfileAsync(Guid userId);
    }
}
