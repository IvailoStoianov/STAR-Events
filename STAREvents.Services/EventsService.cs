using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Events;
using static STAREvents.Common.ErrorMessagesConstants.EventsServiceErrorMessages;
using static STAREvents.Common.FilePathConstants.EventPicturePaths;
using static STAREvents.Common.EntityValidationConstants.RoleNames;
using STAREvents.Services.Data;

public class EventsService : EventHelperService, IEventsService
{
    private readonly IRepository<Event, object> eventRepository;
    private readonly IRepository<Category, object> categoryRepository;
    private readonly IRepository<Comment, object> commentRepository;
    private readonly IRepository<UserEventAttendance, object> attendanceRepository;
    private readonly IUserAuthService userAuthService;
    private readonly IWebHostEnvironment webHostEnvironment;

    public EventsService(
        IRepository<Event, object> eventRepository,
        IRepository<Category, object> categoryRepository,
        IRepository<Comment, object> commentRepository,
        IRepository<UserEventAttendance, object> attendanceRepository,
        IUserAuthService userAuthService,
        IWebHostEnvironment webHostEnvironment)
        : base(eventRepository, attendanceRepository, userAuthService)
    {
        this.eventRepository = eventRepository;
        this.categoryRepository = categoryRepository;
        this.commentRepository = commentRepository;
        this.attendanceRepository = attendanceRepository;
        this.userAuthService = userAuthService;
        this.webHostEnvironment = webHostEnvironment;
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
            throw new KeyNotFoundException(EventNotFound);
        }

        var hasJoined = false;
        if (!string.IsNullOrEmpty(userName))
        {
            var user = await userAuthService.GetUserByNameAsync(userName);
            if (user != null)
            {
                hasJoined = await attendanceRepository.FirstOrDefaultAsync(a => a.EventId == eventId && a.UserId == user.Id) != null;
            }
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
                User = c.User,
                isDeleted = c.isDeleted
            }).ToList(),
            HasJoined = hasJoined
        };
    }

    public async Task AddCommentAsync(Guid eventId, string userName, string content)
    {
        var user = await userAuthService.GetUserByNameAsync(userName);
        if (user == null)
        {
            throw new KeyNotFoundException(UserNotFound);
        }

        var newComment = new Comment
        {
            EventId = eventId,
            UserId = user.Id,
            Content = content,
            PostedDate = DateTime.UtcNow
        };
        await commentRepository.AddAsync(newComment);
    }

    public async Task SoftDeleteCommentAsync(Guid commentId, Guid userId)
    {
        var user = await userAuthService.GetUserByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new KeyNotFoundException(UserNotFound);
        }

        var comment = await commentRepository.FirstOrDefaultAsync(c => c.CommentId == commentId);
        var isAdmin = await userAuthService.IsUserInRoleAsync(userId.ToString(), Administrator);

        if (comment != null && (isAdmin || comment.UserId == userId))
        {
            comment.isDeleted = true;
            await commentRepository.UpdateAsync(comment);
        }
    }

    public async Task<EditEventInputModel> GetEditEventAsync(Guid eventId)
    {
        var eventEntity = await eventRepository.GetByIdAsync(eventId);

        if (eventEntity == null)
        {
            throw new KeyNotFoundException(EventNotFound);
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
            throw new KeyNotFoundException(EventNotFound);
        }

        eventEntity.Name = model.Name;
        eventEntity.Description = model.Description;
        eventEntity.Address = model.Address;
        eventEntity.StartDate = model.StartDate;
        eventEntity.EndDate = model.EndDate;
        eventEntity.CategoryID = model.CategoryId;
        eventEntity.Capacity = model.Capacity;

        if (model.Image != null)
        {
            var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, DefaultEventPicturePath);
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.Image.CopyToAsync(fileStream);
            }
            eventEntity.ImageUrl = $"/{DefaultEventPicturePath}/{uniqueFileName}";
        }
        await eventRepository.UpdateAsync(eventEntity);
        return eventEntity.EventId;
    }

    public async Task SoftDeleteEventAsync(Guid eventId)
    {
        var eventEntity = await eventRepository.GetByIdAsync(eventId);
        if (eventEntity == null)
        {
            throw new KeyNotFoundException(EventNotFound);
        }

        eventEntity.isDeleted = true;

        var comments = await commentRepository.GetAllAsync();
        var eventComments = comments.Where(c => c.EventId == eventId).ToList();
        foreach (var comment in eventComments)
        {
            comment.isDeleted = true;
            await commentRepository.UpdateAsync(comment);
        }

        await eventRepository.UpdateAsync(eventEntity);
    }
}
