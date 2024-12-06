using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EventsService : IEventsService
{
    private IRepository<Event, object> eventRepository;
    private IRepository<Category, object> categoryRepository;
    private IRepository<Comment, object> commentRepository;
    private IRepository<UserEventAttendance, object> attendanceRepository;
    private readonly UserManager<ApplicationUser> userManager;

    public EventsService(IRepository<Event, object> _eventRepository,
        IRepository<Category, object> categoryRepository,
        IRepository<Comment, object> commentRepository,
        IRepository<UserEventAttendance, object> attendanceRepository,
        UserManager<ApplicationUser> userManager)
    {
        this.eventRepository = _eventRepository;
        this.categoryRepository = categoryRepository;
        this.commentRepository = commentRepository;
        this.attendanceRepository = attendanceRepository;
        this.userManager = userManager;
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
            "MostPopular" => filteredEvents.OrderBy(e => e.NumberOfParticipants),
            "Recent" => filteredEvents.OrderBy(e => e.StartDate),
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

    public async Task<EventViewModel> GetEventDetailsAsync(Guid eventId, string userName)
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

        var user = await userManager.FindByNameAsync(userName);
        var hasJoined = await attendanceRepository.FirstOrDefaultAsync(a => a.EventId == eventId && a.UserId == user.Id) != null;

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

    public async Task JoinEventAsync(Guid eventId, string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        var eventEntity = await eventRepository.GetByIdAsync(eventId);
        var attendance = await attendanceRepository.FirstOrDefaultAsync(a => a.EventId == eventId && a.UserId == user.Id);

        if (attendance == null)
        {
            var newAttendance = new UserEventAttendance
            {
                EventId = eventId,
                UserId = user.Id,
                JoinedDate = DateTime.UtcNow
            };
            await attendanceRepository.AddAsync(newAttendance);
            eventEntity.NumberOfParticipants++;
            await eventRepository.UpdateAsync(eventEntity);
        }
    }

    public async Task LeaveEventAsync(Guid eventId, string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        var eventEntity = await eventRepository.GetByIdAsync(eventId);
        var attendance = await attendanceRepository.FirstOrDefaultAsync(a => a.EventId == eventId && a.UserId == user.Id);

        if (attendance != null)
        {
            await attendanceRepository.DeleteAsync(attendance);
            eventEntity.NumberOfParticipants--;
            await eventRepository.UpdateAsync(eventEntity);
        }
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
}
