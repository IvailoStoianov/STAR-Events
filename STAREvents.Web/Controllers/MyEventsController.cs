using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STAREvents.Services.Data;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Events;
using System.Security.Claims;

namespace STAREvents.Web.Controllers
{
    [Authorize]
    public class MyEventsController : Controller
    {
        private readonly IMyEventsService myEventsService;
        public MyEventsController(IMyEventsService _myEventsService)
        {
            this.myEventsService = _myEventsService;
        }
        [HttpGet]
        public IActionResult All(string searchTerm, Guid? selectedCategory, string sortOption, int page = 1)
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int pageSize = 8;
            EventsViewModel model = myEventsService.LoadMyEventsAsync(searchTerm, selectedCategory, sortOption, userId, page, pageSize).Result;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> JoinEvent(Guid eventId)
        {
            await myEventsService.JoinEventAsync(eventId, User?.Identity?.Name);
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> LeaveEvent(Guid eventId)
        {
            await myEventsService.LeaveEventAsync(eventId, User?.Identity?.Name);
            return RedirectToAction(nameof(All));
        }
    }
}
