using STAREvents.Common;
using STAREvents.Data.Models;
using STAREvents.Web.ViewModels.CreateEvents;
using STAREvents.Web.ViewModels.Events;

namespace STAREvents.Services.Data.Interfaces
{
    public interface IEventsService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<ServiceResult<EventsViewModel>> LoadEventsAsync(string searchTerm, Guid? selectedCategory, string sortOption, int page = 1, int pageSize = 12);
        Task<ServiceResult<EventsViewModel>> LoadMyEventsAsync(string searchTerm, Guid? selectedCategory, string sortOption, string userId, int page = 1, int pageSize = 12);
        Task<ServiceResult<EventViewModel>> GetEventDetailsAsync(Guid eventId, string userName = "");
        Task<ServiceResult<Guid>> CreateEventAsync(CreateEventInputModel model, Guid userId);
        Task<ServiceResult<EditEventInputModel>> GetEditEventAsync(Guid eventId);
        Task<ServiceResult<Guid>> EditEventAsync(EditEventInputModel model);
        Task<ServiceResult> SoftDeleteEventAsync(Guid eventId);
        Task<ServiceResult> JoinEventAsync(Guid eventId, Guid userId);
        Task<ServiceResult> LeaveEventAsync(Guid eventId, Guid userId);
        Task<ServiceResult> AddCommentAsync(Guid eventId, string userName, string content);
        Task<ServiceResult> SoftDeleteCommentAsync(Guid commentId, Guid userId);
    }
}
