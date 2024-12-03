using STAREvents.Web.ViewModels.CreateEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Services.Data.Interfaces
{
    public interface ICreateEventsService
    {
        Task CreateEventAsync(CreateEventInputModel model);
    }
}
