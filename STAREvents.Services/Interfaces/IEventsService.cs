using Microsoft.AspNetCore.Mvc;
using STAREvents.Web.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Services.Data.Interfaces
{
    public interface IEventsService : IEventHelperService
    {
        Task<EventsViewModel> LoadEventAsync(string searchTerm, Guid? selectedCategory, string sortOption, int page = 1, int pageSize = 12);
        Task<EventViewModel> GetEventDetailsAsync(Guid eventId, string userName = "");
        Task AddCommentAsync(Guid eventId, string userName, string content);
        Task DeleteCommentAsync(Guid commentId, string userName);
        Task<EditEventInputModel> GetEditEventAsync(Guid eventId);
        Task<Guid> EditEventAsync(EditEventInputModel model);
    }
}
