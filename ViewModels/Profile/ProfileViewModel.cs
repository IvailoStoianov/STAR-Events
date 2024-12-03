using STAREvents.Data.Models;
using STAREvents.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Web.ViewModels.Profile
{
    public class ProfileViewModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public byte[]? ProfilePicture { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
