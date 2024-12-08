using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STAREvents.Services.Data.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace STAREvents.Web.Controllers
{
    [Authorize]
    public class MyEventsController : Controller
    {
        private readonly IMyEventsService myEventsService;

        public MyEventsController(IMyEventsService myEventsService)
        {
            this.myEventsService = myEventsService;
        }

        [HttpGet]
        public async Task<IActionResult> All(string searchTerm, Guid? selectedCategory, string sortOption, int page = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var model = await myEventsService.LoadMyEventsAsync(searchTerm, selectedCategory, sortOption, userId, page);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> JoinEvent(Guid eventId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            await myEventsService.JoinEventAsync(eventId, Guid.Parse(userId));
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> LeaveEvent(Guid eventId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            await myEventsService.LeaveEventAsync(eventId, Guid.Parse(userId));
            return RedirectToAction(nameof(All));
        }
    }
}





