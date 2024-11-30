using STAREvents.Web.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Services.Data.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileViewModel> LoadProfileAsync(Guid userId);
        Task<ProfileInputModel> LoadEditFormAsync(Guid userId);
    }
}
