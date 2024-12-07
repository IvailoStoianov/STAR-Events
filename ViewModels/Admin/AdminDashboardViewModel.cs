using STAREvents.Web.ViewModels.Events;
using STAREvents.Web.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Web.ViewModels.Admin
{
    public class AdminDashboardViewModel
    {
        public List<EventViewModel> Events { get; set; }
        public List<ProfileViewModel> Users { get; set; }
        public int TotalEvents { get; set; }
        public int UpcomingEvents { get; set; }
        public int PastEvents { get; set; }
        public int TotalUsers { get; set; }
    }
}
