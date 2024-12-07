using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Services.Data.Interfaces
{
    public interface IEventHelperService 
    {
        Task JoinEventAsync(Guid eventId, string userName);
        Task LeaveEventAsync(Guid eventId, string userName);
    }
}
