using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Profile;
using System.Security.Claims;
using static STAREvents.Common.SuccessMessage.Profile;

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

            var userId = Guid.Parse(userIdValue);
            var result = await profileService.LoadProfileAsync(userId);

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault();
                return RedirectToAction("Index", "Home");
            }

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdValue == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdValue);
            var result = await profileService.LoadEditFormAsync(userId);

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault();
                return RedirectToAction(nameof(Index));
            }

            return View(result.Data);
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

            var userId = Guid.Parse(userIdValue);
            var result = await profileService.UpdateProfileAsync(userId, model);

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault();
                return View(model);
            }

            TempData["SuccessMessage"] = ProfileUpdated;
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

            var userId = Guid.Parse(userIdValue);
            var result = await profileService.ChangePasswordAsync(userId, model);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(model);
            }

            TempData["SuccessMessage"] = PasswordChanged;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProfile()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdValue == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdValue);
            var result = await profileService.SoftDeleteProfileAsync(userId);

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault();
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = ProfileDeleted;
            return RedirectToAction("Index", "Home");
        }
    }
}