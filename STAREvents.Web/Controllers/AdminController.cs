using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STAREvents.Services.Data;
using STAREvents.Services.Data.Interfaces.STAREvents.Web.Services;
using STAREvents.Web.ViewModels.Admin;
using STAREvents.Web.ViewModels.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace STAREvents.Web.Controllers
{
    [Authorize(Roles = "Admin")]
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
            var model = await _adminService.GetAdminDashboardViewModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            await _adminService.SoftDeleteEventAsync(id);
            return RedirectToAction(nameof(Dashboard));
        }
        [HttpPost]
        public async Task<IActionResult> RecoverEvent(Guid id)
        {
            await _adminService.RecoverEventAsync(id);
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _adminService.SoftDeleteUserAsync(id);
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> RecoverUser(Guid id)
        {
            await _adminService.RecoverUserAsync(id);
            return RedirectToAction(nameof(Dashboard));
        }
    }
}
