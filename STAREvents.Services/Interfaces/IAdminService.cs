using STAREvents.Web.ViewModels.Profile;
using global::STAREvents.Web.ViewModels.Events;
using STAREvents.Web.ViewModels.Admin;

namespace STAREvents.Services.Data.Interfaces
{
    namespace STAREvents.Web.Services
    {
        public interface IAdminService
        {
            Task<AdminDashboardViewModel> GetAdminDashboardViewModelAsync();
            Task<List<EventViewModel>> GetAllEventsAsync();
            Task<List<ProfileViewModel>> GetAllUsersAsync();
            Task<int> GetTotalEventsAsync();
            Task<int> GetUpcomingEventsCountAsync();
            Task<int> GetPastEventsCountAsync();
            Task<int> GetTotalUsersAsync();
            Task SoftDeleteEventAsync(Guid eventId);
            Task SoftDeleteUserAsync(Guid userId);
            Task RecoverUserAsync(Guid userId);
            Task RecoverEventAsync(Guid id);
        }
    }

}