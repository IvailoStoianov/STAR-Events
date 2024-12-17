using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STAREvents.Services.Data.Interfaces;
using static STAREvents.Common.EntityValidationConstants.RoleNames;
using static STAREvents.Common.ErrorMessagesConstants.AdminControllerErrorMessages;
using static STAREvents.Common.SuccessMessage.Admin;

namespace STAREvents.Web.Areas.Admin.Controllers
{
    [Area(Administrator)]
    [Authorize(Roles = Administrator)]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var result = await _adminService.GetAdminDashboardViewModelAsync();
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault() ?? FailedToLoadDashboardData;
                return View("Error");
            }
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var result = await _adminService.SoftDeleteEventAsync(id);
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault() ?? FailedToDeleteEvent;
            }
            else
            {
                TempData["SuccessMessage"] = EventDeleted;
            }
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> RecoverEvent(Guid id)
        {
            var result = await _adminService.RecoverEventAsync(id);
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault() ?? FailedToRecoverEvent;
            }
            else
            {
                TempData["SuccessMessage"] = EventRecovered;
            }
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _adminService.SoftDeleteUserAsync(id);
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault() ?? FailedToDeleteUser;
            }
            else
            {
                TempData["SuccessMessage"] = UserDeleted;
            }
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> RecoverUser(Guid id)
        {
            var result = await _adminService.RecoverUserAsync(id);
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault() ?? FailedToRecoverUser;
            }
            else
            {
                TempData["SuccessMessage"] = UserRecovered;
            }
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> MakeAdmin(Guid id)
        {
            var result = await _adminService.AddAdminRole(id);
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault() ?? FailedToGrantAdminRole;
            }
            else
            {
                TempData["SuccessMessage"] = AdminRoleAdded;
            }
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdmin(Guid id)
        {
            var result = await _adminService.RemoveAdminRole(id);
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault() ?? FailedToRemoveAdminRole;
            }
            else
            {
                TempData["SuccessMessage"] = AdminRoleRemoved;
            }
            return RedirectToAction(nameof(Dashboard));
        }
    }
}
