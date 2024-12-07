using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services;
using STAREvents.Services.Data;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EventsService : EventHelperService, IEventsService
{
    private IRepository<Event, object> eventRepository;
    private IRepository<Category, object> categoryRepository;
    private IRepository<Comment, object> commentRepository;
    private IRepository<UserEventAttendance, object> attendanceRepository;
    private readonly UserManager<ApplicationUser> userManager;
    private IWebHostEnvironment webHostEnvironment;

    public EventsService(IRepository<Event, object> _eventRepository,
        IRepository<Category, object> _categoryRepository,
        IRepository<Comment, object> _commentRepository,
        IRepository<UserEventAttendance, object> _attendanceRepository,
        UserManager<ApplicationUser> _userManager,
        IWebHostEnvironment _webHostEnvironment)
        : base(_eventRepository, _attendanceRepository, _userManager)
    {
        this.eventRepository = _eventRepository;
        this.categoryRepository = _categoryRepository;
        this.commentRepository = _commentRepository;
        this.attendanceRepository = _attendanceRepository;
        this.userManager = _userManager;
        this.webHostEnvironment = _webHostEnvironment;
    }

    public async Task<EventsViewModel> LoadEventAsync(string searchTerm, Guid? selectedCategory, string sortOption, int page = 1, int pageSize = 12)
    {
        var events = await eventRepository.GetAllAsync();
        var categories = await categoryRepository.GetAllAsync();

        var filteredEvents = events
            .Where(e => string.IsNullOrEmpty(searchTerm) || e.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .Where(e => !selectedCategory.HasValue || e.Category.CategoryID == selectedCategory)
            .Where(e => e.isDeleted == false);

        filteredEvents = sortOption switch
        {
            "MostPopular" => filteredEvents.OrderByDescending(e => e.NumberOfParticipants),
            "Recent" => filteredEvents.OrderByDescending(e => e.StartDate),
            "Oldest" => filteredEvents.OrderBy(e => e.StartDate),
            "Alphabetical" => filteredEvents.OrderBy(e => e.Name),
            _ => filteredEvents.OrderBy(e => e.Name)
        };

        var totalEvents = filteredEvents.Count();
        var paginatedEvents = filteredEvents
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var eventViewModels = paginatedEvents.Select(e => new EventViewModel
        {
            EventId = e.EventId,
            Name = e.Name,
            Description = e.Description,
            ImageUrl = e.ImageUrl,
            CreatedOnDate = e.CreatedOnDate,
            StartDate = e.StartDate,
            EndDate = e.EndDate,
            Capacity = e.Capacity,
            NumberOfParticipants = e.NumberOfParticipants,
            isDeleted = e.isDeleted,
            OrganizerID = e.OrganizerID,
            Organizer = e.Organizer,
            Category = e.Category
        }).ToList();

        return new EventsViewModel
        {
            Events = eventViewModels,
            Categories = categories,
            SearchTerm = searchTerm,
            SelectedCategory = selectedCategory,
            SortOption = sortOption,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalEvents / (double)pageSize)
        };
    }

    public async Task<EventViewModel> GetEventDetailsAsync(Guid eventId, string userName = "")
    {
        var eventEntity = await eventRepository
            .GetAllAttached()
            .Include(e => e.Category)
            .Include(e => e.Organizer)
            .Include(e => e.EventComments).ThenInclude(c => c.User)
            .FirstOrDefaultAsync(e => e.EventId == eventId);

        if (eventEntity == null)
        {
            return null;
        }

        var hasJoined = false;
        if (userName != null)
        {
            var user = await userManager.FindByNameAsync(userName);
            hasJoined = await attendanceRepository.FirstOrDefaultAsync(a => a.EventId == eventId && a.UserId == user.Id) != null;
        }

        return new EventViewModel
        {
            EventId = eventEntity.EventId,
            Name = eventEntity.Name,
            Description = eventEntity.Description,
            ImageUrl = eventEntity.ImageUrl,
            CreatedOnDate = eventEntity.CreatedOnDate,
            StartDate = eventEntity.StartDate,
            EndDate = eventEntity.EndDate,
            Capacity = eventEntity.Capacity,
            NumberOfParticipants = eventEntity.NumberOfParticipants,
            isDeleted = eventEntity.isDeleted,
            OrganizerID = eventEntity.OrganizerID,
            Address = eventEntity.Address,
            Organizer = eventEntity.Organizer,
            Category = eventEntity.Category,
            Comments = eventEntity.EventComments.Select(c => new CommentViewModel
            {
                CommentId = c.CommentId,
                Content = c.Content,
                PostedDate = c.PostedDate,
                User = c.User
            }).ToList(),
            HasJoined = hasJoined 
        };
    }


    public async Task AddCommentAsync(Guid eventId, string userName, string content)
    {
        var user = await userManager.FindByNameAsync(userName);
        var newComment = new Comment
        {
            EventId = eventId,
            UserId = user.Id,
            Content = content,
            PostedDate = DateTime.UtcNow
        };
        await commentRepository.AddAsync(newComment);
    }

    public async Task DeleteCommentAsync(Guid commentId, string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        var comment = await commentRepository.FirstOrDefaultAsync(c => c.CommentId == commentId && (c.UserId == user.Id || c.Event.OrganizerID == user.Id));

        if (comment != null)
        {
            await commentRepository.DeleteAsync(comment);
        }
    }

    public async Task<EditEventInputModel> GetEditEventAsync(Guid eventId)
    {
        var eventEntity = await eventRepository.GetByIdAsync(eventId);

        if (eventEntity == null)
        {
            return null;
        }

        return new EditEventInputModel
        {
            EventId = eventEntity.EventId,
            Name = eventEntity.Name,
            Description = eventEntity.Description,
            Address = eventEntity.Address,
            CreatedOnDate = eventEntity.CreatedOnDate,
            StartDate = eventEntity.StartDate,
            EndDate = eventEntity.EndDate,
            CategoryId = eventEntity.CategoryID,
            Categories = await categoryRepository.GetAllAsync(),
            Capacity = eventEntity.Capacity
        };
    }
    public async Task<Guid> EditEventAsync(EditEventInputModel model)
    {
        var eventEntity = await eventRepository.GetByIdAsync(model.EventId);
        if (eventEntity == null)
        {
            throw new Exception("Event not found.");
        }
        eventEntity.Name = model.Name;
        eventEntity.Description = model.Description;
        eventEntity.Address = model.Address;
        eventEntity.StartDate = model.StartDate;
        eventEntity.EndDate = model.EndDate;
        eventEntity.CategoryID = model.CategoryId;
        eventEntity.Capacity = model.Capacity;

        if(model.Image != null)
        {
            var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/event-images");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.Image.CopyToAsync(fileStream);
            }
            eventEntity.ImageUrl = $"/images/event-images/{uniqueFileName}";
        }
        await eventRepository.UpdateAsync(eventEntity);
        return eventEntity.EventId;
    }
}
