using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.Models;
using STAREvents.Web.ViewModels.Profile;
using System.Security.Claims;
using static STAREvents.Common.ErrorMessagesConstants.ProfileControllerErrorMessages;


namespace STAREvents.Web.Controllers
{
    //TODO: Add a logger/framework for logging
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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return View("Error", new ErrorViewModel { Message = ProfileLoadError });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            try
            {
                var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userIdValue == null)
                {
                    return Unauthorized();
                }

                Guid userId = new Guid(userIdValue);
                ProfileInputModel model = await profileService.LoadEditFormAsync(userId);
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return View("Error", new ErrorViewModel { Message = EditFormLoadError });
            }
        }

    }
}
