using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STAREvents.Services.Data.Interfaces;
using System.Security.Claims;
using static STAREvents.Common.ErrorMessagesConstants.NotifcationsControllerErrorMessages;

namespace STAREvents.Web.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly INotificationsService _notificationsService;

        public NotificationsController(INotificationsService notificationsService)
        {
            _notificationsService = notificationsService;
        }

        [HttpGet]
        public async Task<IActionResult> MyNotifications()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var result = await _notificationsService.GetUserNotificationsAsync(Guid.Parse(userId));

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault() ?? FailedToLoadNotifications;
                return View(new List<ViewModels.Notifications.NotificationViewModel>());
            }

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(Guid notificationId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var result = await _notificationsService.MarkAsReadAsync(notificationId, Guid.Parse(userId));
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault() ?? FailedToMarkNotifcationAsRead;
            }

            return RedirectToAction(nameof(MyNotifications));
        }
    }
}
