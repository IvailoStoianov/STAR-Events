using STAREvents.Web.ViewModels.Profile;
using STAREvents.Web.ViewModels.Events;
using STAREvents.Web.ViewModels.Admin;
using STAREvents.Common;

namespace STAREvents.Services.Data.Interfaces
{
    public interface IAdminService
    {
        Task<ServiceResult<AdminDashboardViewModel>> GetAdminDashboardViewModelAsync();
        Task<ServiceResult<List<EventViewModel>>> GetAllEventsAsync();
        Task<ServiceResult<List<ProfileViewModel>>> GetAllUsersAsync();
        Task<ServiceResult> SoftDeleteEventAsync(Guid eventId);
        Task<ServiceResult> SoftDeleteUserAsync(Guid userId);
        Task<ServiceResult> RecoverUserAsync(Guid userId);
        Task<ServiceResult> RecoverEventAsync(Guid id);
        Task<ServiceResult<List<CommentViewModel>>> GetEventCommentsAsync(Guid eventId);
        Task<ServiceResult> SoftDeleteCommentAsync(Guid commentId, string userName);
        Task<ServiceResult> AddAdminRole(Guid userId);
        Task<ServiceResult> RemoveAdminRole(Guid userId);
    }
}
