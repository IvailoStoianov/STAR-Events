using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Events;
using System.Security.Claims;
using System.Threading.Tasks;

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
        string? userName = User?.Identity?.Name;
        var eventDetails = await _eventsService.GetEventDetailsAsync(id, userName);
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }
        await _eventsService.JoinEventAsync(eventId, Guid.Parse(userId));
        return RedirectToAction(nameof(EventDetails), new { id = eventId });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> LeaveEvent(Guid eventId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }
        await _eventsService.LeaveEventAsync(eventId, Guid.Parse(userId));
        return RedirectToAction(nameof(EventDetails), new { id = eventId });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddComment(Guid eventId, string content)
    {
        string? userName = User?.Identity?.Name;
        if (string.IsNullOrEmpty(userName))
        {
            return Unauthorized();
        }
        await _eventsService.AddCommentAsync(eventId, userName, content);
        return RedirectToAction(nameof(EventDetails), new { id = eventId });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> DeleteComment(Guid commentId, Guid eventId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }
        await _eventsService.SoftDeleteCommentAsync(commentId, Guid.Parse(userId));
        return RedirectToAction(nameof(EventDetails), new { id = eventId });
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
        return RedirectToAction(nameof(EventDetails), new { id = eventId });
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> SoftDeleteEvent(Guid eventId)
    {
        await _eventsService.SoftDeleteEventAsync(eventId);
        return RedirectToAction(nameof(All), new { id = eventId });
    }
}
