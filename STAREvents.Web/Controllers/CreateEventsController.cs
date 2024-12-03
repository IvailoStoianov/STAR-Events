using Microsoft.AspNetCore.Mvc;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.CreateEvents;

namespace STAREvents.Web.Controllers
{
    public class CreateEventsController : Controller
    {
        private readonly ICreateEventsService createEventsService;

        public CreateEventsController(ICreateEventsService _createEventsService)
        {
            this.createEventsService = _createEventsService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await createEventsService.CreateEventAsync(model);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }
    }
}
