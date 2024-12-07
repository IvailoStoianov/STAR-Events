using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Events;

public class EventsController : Controller
{
    private readonly IEventsService _eventsService;

    public EventsController(IEventsService eventsService)
    {
        _eventsService = eventsService;
    }

    [HttpGet]
    public async Task<IActionResult> All(string searchTerm, Guid? selectedCategory, string sortOption, int page = 1)
    {
        var model = await _eventsService.LoadEventAsync(searchTerm, selectedCategory, sortOption, page);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EventDetails(Guid id)
    {
        var eventDetails = await _eventsService.GetEventDetailsAsync(id, User?.Identity?.Name);
        if (eventDetails == null)
        {
            return NotFound();
        }
        return View(eventDetails);
    }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> JoinEvent(Guid eventId)
    {
        await _eventsService.JoinEventAsync(eventId, User?.Identity?.Name);
        return RedirectToAction("EventDetails", new { id = eventId });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> LeaveEvent(Guid eventId)
    {
        await _eventsService.LeaveEventAsync(eventId, User?.Identity?.Name);
        return RedirectToAction("EventDetails", new { id = eventId });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddComment(Guid eventId, string content)
    {
        await _eventsService.AddCommentAsync(eventId, User?.Identity?.Name, content);
        return RedirectToAction("EventDetails", new { id = eventId });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> DeleteComment(Guid commentId, Guid eventId)
    {
        await _eventsService.DeleteCommentAsync(commentId, User.Identity.Name);
        return RedirectToAction("EventDetails", new { id = eventId });
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Edit(Guid eventId)
    {
        EditEventInputModel model = await _eventsService.GetEditEventAsync(eventId);
        return View(model);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Edit(EditEventInputModel model)
    {
        Guid eventId = await _eventsService.EditEventAsync(model);
        return RedirectToAction("EventDetails", new { id = eventId });
    }
}
