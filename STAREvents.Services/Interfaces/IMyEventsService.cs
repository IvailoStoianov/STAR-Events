using Microsoft.AspNetCore.Http;
using STAREvents.Web.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Services.Data.Interfaces
{
    public interface IMyEventsService : IEventHelperService
    {
        Task<EventsViewModel> LoadMyEventsAsync(string searchTerm, Guid? selectedCategory, string sortOption, string userId, int page = 1, int pageSize = 12);
    }
}
