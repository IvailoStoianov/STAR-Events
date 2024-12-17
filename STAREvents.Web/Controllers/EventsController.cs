using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.CreateEvents;
using STAREvents.Web.ViewModels.Events;
using System.Security.Claims;
using STAREvents.Common; 
using static STAREvents.Common.EntityValidationConstants;

namespace STAREvents.Web.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly IEventsService _eventsService;
        private int _pageSize;

        public EventsController(IEventsService eventsService)
        {
            _eventsService = eventsService;
            _pageSize = PageSizeConstants.DefaultPageSize;
        }

        [HttpGet]
        public async Task<IActionResult> All(string searchTerm, Guid? selectedCategory, string sortOption, int page = 1)
        {
            var result = await _eventsService.LoadEventsAsync(searchTerm, selectedCategory, sortOption, page, _pageSize);

            if (!result.Succeeded)
                return View("Error", result.Errors);

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> MyEvents(string searchTerm, Guid? selectedCategory, string sortOption, int page = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var result = await _eventsService.LoadMyEventsAsync(searchTerm, selectedCategory, sortOption, userId, page, _pageSize);

            if (!result.Succeeded)
                return View("Error", result.Errors);

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> EventDetails(Guid id)
        {
            string userName = User?.Identity?.Name ?? string.Empty;
            var result = await _eventsService.GetEventDetailsAsync(id, userName);

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _eventsService.GetCategoriesAsync();
            var model = new CreateEventInputModel { Categories = categories.ToList() };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventInputModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _eventsService.GetCategoriesAsync();
                return View(model);
            }

            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdValue == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdValue);
            var result = await _eventsService.CreateEventAsync(model, userId);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error);

                model.Categories = await _eventsService.GetCategoriesAsync();
                return View(model);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid eventId)
        {
            var result = await _eventsService.GetEditEventAsync(eventId);

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEventInputModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = (await _eventsService.GetCategoriesAsync()).ToList();
                return View(model);
            }

            var result = await _eventsService.EditEventAsync(model);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error);

                model.Categories = (await _eventsService.GetCategoriesAsync()).ToList();
                return View(model);
            }

            return RedirectToAction(nameof(EventDetails), new { id = result.Data });
        }

        [HttpPost]
        public async Task<IActionResult> SoftDeleteEvent(Guid eventId)
        {
            var result = await _eventsService.SoftDeleteEventAsync(eventId);

            if (!result.Succeeded)
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> JoinEvent(Guid eventId)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdValue == null)
            {
                return Unauthorized();
            }

            var result = await _eventsService.JoinEventAsync(eventId, Guid.Parse(userIdValue));

            if (!result.Succeeded)
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault();

            return RedirectToAction(nameof(EventDetails), new { id = eventId });
        }

        [HttpPost]
        public async Task<IActionResult> LeaveEvent(Guid eventId)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdValue == null)
            {
                return Unauthorized();
            }

            var result = await _eventsService.LeaveEventAsync(eventId, Guid.Parse(userIdValue));

            if (!result.Succeeded)
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault();

            return RedirectToAction(nameof(EventDetails), new { id = eventId });
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Guid eventId, string content)
        {
            string? userName = User?.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
                return Unauthorized();

            var result = await _eventsService.AddCommentAsync(eventId, userName, content);

            if (!result.Succeeded)
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault();

            return RedirectToAction(nameof(EventDetails), new { id = eventId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(Guid commentId, Guid eventId)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdValue == null)
            {
                return Unauthorized();
            }

            var result = await _eventsService.SoftDeleteCommentAsync(commentId, Guid.Parse(userIdValue));

            if (!result.Succeeded)
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault();

            return RedirectToAction(nameof(EventDetails), new { id = eventId });
        }
    }
}
