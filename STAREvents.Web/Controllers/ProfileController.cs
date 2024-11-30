using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Profile;
using System.Security.Claims;


namespace STAREvents.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;
        public ProfileController(IProfileService _profileService)
        {
            this.profileService = _profileService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdValue == null)
            {
                return Unauthorized();
            }

            Guid userId = new Guid(userIdValue);
            ProfileViewModel model = await profileService.LoadProfileAsync(userId);
            return View(model);
        }
    }
}
