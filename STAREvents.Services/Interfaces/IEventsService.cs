using STAREvents.Web.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Services.Data.Interfaces
{
    public interface IEventsService
    {
        Task<EventsViewModel> LoadEventAsync(string searchTerm, Guid? selectedCategory, string sortOption, int page = 1, int pageSize = 12);
    }
}
