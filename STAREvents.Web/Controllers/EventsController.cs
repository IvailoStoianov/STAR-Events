using Microsoft.AspNetCore.Mvc;
using STAREvents.Services.Data.Interfaces;

namespace STAREvents.Web.Controllers
{
    public class EventsController : Controller
    {

        private readonly IEventsService _eventsService;

        public EventsController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        public async Task<IActionResult> All(string searchTerm, Guid? selectedCategory, string sortOption, int page = 1)
        {
            var model = await _eventsService.LoadEventAsync(searchTerm, selectedCategory, sortOption, page);
            return View(model);
        }
    }
}
