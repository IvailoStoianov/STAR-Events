using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.Models;
using STAREvents.Web.ViewModels.Profile;
using System.Security.Claims;
using static STAREvents.Common.ErrorMessagesConstants.ProfileControllerErrorMessages;

namespace STAREvents.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
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

        [HttpGet]
        public async Task<IActionResult> Edit()
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

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdValue == null)
            {
                return Unauthorized();
            }

            Guid userId = new Guid(userIdValue);
            await profileService.UpdateProfileAsync(userId, model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdValue == null)
            {
                return Unauthorized();
            }

            var userId = new Guid(userIdValue);
            var result = await profileService.ChangePasswordAsync(userId, model);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("CurrentPassword", error.Description);
            }

            return View(model);
        }
    }
}





