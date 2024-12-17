using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Events;
using STAREvents.Web.ViewModels.CreateEvents;
using static STAREvents.Common.ErrorMessagesConstants;
using static STAREvents.Common.FilePathConstants;
using static STAREvents.Common.ModelErrorsConstants;
using STAREvents.Common;
using Microsoft.Extensions.Configuration;

namespace STAREvents.Services.Data
{
    public class EventsService : IEventsService
    {
        private readonly IRepository<Event, object> _eventRepository;
        private readonly IRepository<Category, object> _categoryRepository;
        private readonly IRepository<Comment, object> _commentRepository;
        private readonly IRepository<UserEventAttendance, object> _attendanceRepository;
        private readonly IUserAuthService _userAuthService;
        private readonly IFileStorageService _fileStorageService;
        private readonly bool useAzureBlobStorage;


        public EventsService(
            IRepository<Event, object> eventRepository,
            IRepository<Category, object> categoryRepository,
            IRepository<Comment, object> commentRepository,
            IRepository<UserEventAttendance, object> attendanceRepository,
            IUserAuthService userAuthService,
            IFileStorageService fileStorageService,
            IConfiguration configuration)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
            _attendanceRepository = attendanceRepository;
            _userAuthService = userAuthService;
            _fileStorageService = fileStorageService;

            useAzureBlobStorage = configuration.GetValue<bool>("UseAzureBlobStorage");
        }

        public async Task<ServiceResult<EventsViewModel>> LoadEventsAsync(string searchTerm, Guid? selectedCategory, string sortOption, int page, int pageSize)
        {
            var events = await _eventRepository.GetAllAsync();
            var categories = await _categoryRepository.GetAllAsync();

            var filteredEvents = FilterEvents(events, searchTerm, selectedCategory, sortOption);
            var totalEvents = filteredEvents.Count();
            var paginatedEvents = filteredEvents.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var eventViewModels = paginatedEvents.Select(MapToEventViewModel).ToList();

            var viewModel = new EventsViewModel
            {
                Events = eventViewModels,
                Categories = categories,
                SearchTerm = searchTerm,
                SelectedCategory = selectedCategory,
                SortOption = sortOption,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalEvents / (double)pageSize)
            };

            return ServiceResult<EventsViewModel>.Success(viewModel);
        }

        public async Task<ServiceResult<EventViewModel>> GetEventDetailsAsync(Guid eventId, string userName = "")
        {
            var eventEntity = await _eventRepository.GetAllAttached()
                .Include(e => e.Category)
                .Include(e => e.Organizer)
                .Include(e => e.EventComments).ThenInclude(c => c.User)
                .FirstOrDefaultAsync(e => e.EventId == eventId);

            if (eventEntity == null)
                return ServiceResult<EventViewModel>.Failure(EventsServiceErrorMessages.EventNotFound);

            var hasJoined = false;
            if (!string.IsNullOrEmpty(userName))
            {
                var user = await _userAuthService.GetUserByNameAsync(userName);
                hasJoined = user != null && await _attendanceRepository.FirstOrDefaultAsync(a => a.EventId == eventId && a.UserId == user.Id) != null;
            }

            var result = MapToDetailedEventViewModel(eventEntity, hasJoined);
            return ServiceResult<EventViewModel>.Success(result);
        }

        public async Task<ServiceResult<Guid>> CreateEventAsync(CreateEventInputModel model, Guid userId)
        {
            if (model.StartDate >= model.EndDate)
            {
                return ServiceResult<Guid>.Failure(new List<string> { Date.StartDateBeforeEndDate });
            }

            string imageUrl = null;
            if (model.Image != null)
            {
                imageUrl = useAzureBlobStorage
                    ? await _fileStorageService.UploadFileAsync(model.Image, EventPicturePaths.DefaultEventPicturePath)
                    : await _fileStorageService.UploadFileLocallyAsync(model.Image, EventPicturePaths.DefaultEventPicturePath);
            }

            var newEvent = new Event
            {
                EventId = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Capacity = model.Capacity,
                OrganizerID = userId,
                CategoryID = model.CategoryId,
                Address = model.Address,
                CreatedOnDate = DateTime.UtcNow,
                ImageUrl = imageUrl
            };

            await _eventRepository.AddAsync(newEvent);
            return ServiceResult<Guid>.Success(newEvent.EventId);
        }


        public async Task<ServiceResult<Guid>> EditEventAsync(EditEventInputModel model)
        {
            if (model.StartDate >= model.EndDate)
            {
                return ServiceResult<Guid>.Failure(new List<string> { Date.StartDateBeforeEndDate });
            }

            var eventEntity = await _eventRepository.GetByIdAsync(model.EventId);
            if (eventEntity == null)
            {
                return ServiceResult<Guid>.Failure(new List<string> { EventsServiceErrorMessages.EventNotFound });
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
                eventEntity.ImageUrl = useAzureBlobStorage
                    ? await _fileStorageService.UploadFileAsync(model.Image, EventPicturePaths.DefaultEventPicturePath)
                    : await _fileStorageService.UploadFileLocallyAsync(model.Image, EventPicturePaths.DefaultEventPicturePath);
            }

            await _eventRepository.UpdateAsync(eventEntity);
            return ServiceResult<Guid>.Success(eventEntity.EventId);
        }


        public async Task<ServiceResult> SoftDeleteEventAsync(Guid eventId)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(eventId);
            if (eventEntity == null)
            {
                return ServiceResult.Failure(new List<string> { EventsServiceErrorMessages.EventNotFound });
            }

            eventEntity.isDeleted = true;

            var eventComments = await _commentRepository.GetAllAsync();
            foreach (var comment in eventComments.Where(c => c.EventId == eventId))
            {
                comment.isDeleted = true;
                await _commentRepository.UpdateAsync(comment);
            }

            await _eventRepository.UpdateAsync(eventEntity);
            return ServiceResult.Success();
        }
        public async Task<ServiceResult> JoinEventAsync(Guid eventId, Guid userId)
        {
            var user = await _userAuthService.GetUserByIdAsync(userId.ToString());
            if (user == null)
            {
                return ServiceResult.Failure(new List<string> { EventsServiceErrorMessages.UserNotFound });
            }

            var eventEntity = await _eventRepository.GetByIdAsync(eventId);
            if (eventEntity == null)
            {
                return ServiceResult.Failure(new List<string> { EventsServiceErrorMessages.EventNotFound });
            }

            var attendance = await _attendanceRepository.FirstOrDefaultAsync(a => a.EventId == eventId && a.UserId == user.Id);
            if (attendance == null)
            {
                await _attendanceRepository.AddAsync(new UserEventAttendance
                {
                    EventId = eventId,
                    UserId = user.Id,
                    JoinedDate = DateTime.UtcNow
                });

                eventEntity.NumberOfParticipants++;
                await _eventRepository.UpdateAsync(eventEntity);
            }
            if(eventEntity.Capacity <= eventEntity.NumberOfParticipants)
            {
                return ServiceResult.Failure(new List<string> { EventsServiceErrorMessages.EventFull });
            }

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> LeaveEventAsync(Guid eventId, Guid userId)
        {
            var user = await _userAuthService.GetUserByIdAsync(userId.ToString());
            if (user == null)
                return ServiceResult.Failure(EventsServiceErrorMessages.UserNotFound);

            var attendance = await _attendanceRepository.FirstOrDefaultAsync(a => a.EventId == eventId && a.UserId == user.Id);
            if (attendance != null)
            {
                await _attendanceRepository.DeleteAsync(attendance);

                var eventEntity = await _eventRepository.GetByIdAsync(eventId);
                if (eventEntity != null)
                {
                    eventEntity.NumberOfParticipants--;
                    await _eventRepository.UpdateAsync(eventEntity);
                }
                return ServiceResult.Success();
            }

            return ServiceResult.Failure(EventsServiceErrorMessages.UserNotJoinedEvent);
        }

        public async Task<ServiceResult> AddCommentAsync(Guid eventId, string userName, string content)
        {
            var user = await _userAuthService.GetUserByNameAsync(userName);
            if (user == null)
                return ServiceResult.Failure(EventsServiceErrorMessages.UserNotFound);

            await _commentRepository.AddAsync(new Comment
            {
                EventId = eventId,
                UserId = user.Id,
                Content = content,
                PostedDate = DateTime.UtcNow
            });

            return ServiceResult.Success();
        }


        public async Task<ServiceResult> SoftDeleteCommentAsync(Guid commentId, Guid userId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
                return ServiceResult.Failure(EventsServiceErrorMessages.CommentNotFound);

            var isAdmin = await _userAuthService.IsUserInRoleAsync(userId.ToString(), EntityValidationConstants.RoleNames.Administrator);

            if (isAdmin || comment.UserId == userId)
            {
                comment.isDeleted = true;
                await _commentRepository.UpdateAsync(comment);
                return ServiceResult.Success();
            }

            return ServiceResult.Failure(EventsServiceErrorMessages.UnauthorizedCommentDeletion);
        }


        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<ServiceResult<EditEventInputModel>> GetEditEventAsync(Guid eventId)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(eventId);
            if (eventEntity == null)
            {
                return ServiceResult<EditEventInputModel>.Failure(EventsServiceErrorMessages.EventNotFound);
            }

            var categories = await GetCategoriesAsync();

            var editEventInputModel = new EditEventInputModel
            {
                EventId = eventEntity.EventId,
                Name = eventEntity.Name,
                Description = eventEntity.Description,
                Address = eventEntity.Address,
                StartDate = eventEntity.StartDate,
                EndDate = eventEntity.EndDate,
                CategoryId = eventEntity.CategoryID,
                Capacity = eventEntity.Capacity,
                Categories = categories.ToList()
            };

            return ServiceResult<EditEventInputModel>.Success(editEventInputModel);
        }


        public async Task<ServiceResult<EventsViewModel>> LoadMyEventsAsync(string searchTerm, Guid? selectedCategory, string sortOption, string userId, int page, int pageSize)
        {
            var userGuid = Guid.Parse(userId);
            var events = await _eventRepository.GetAllAttached()
                .Include(e => e.Organizer)
                .Include(e => e.Category)
                .Include(e => e.UserEventAttendances)
                .Where(e => !e.isDeleted && (e.OrganizerID == userGuid || e.UserEventAttendances.Any(uea => uea.UserId == userGuid)))
                .ToListAsync();

            var categories = await GetCategoriesAsync();
            var filteredEvents = FilterEvents(events, searchTerm, selectedCategory, sortOption);

            var totalEvents = filteredEvents.Count();
            var paginatedEvents = filteredEvents.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new EventsViewModel
            {
                Events = paginatedEvents.Select(e => MapToEventViewModel(e)).ToList(),
                Categories = categories.ToList(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalEvents / (double)pageSize)
            };

            return ServiceResult<EventsViewModel>.Success(viewModel);
        }


        private IQueryable<Event> FilterEvents(IEnumerable<Event> events, string searchTerm, Guid? selectedCategory, string sortOption)
        {
            var query = events
                .Where(e => !e.isDeleted)
                .Where(e => string.IsNullOrEmpty(searchTerm) || e.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .Where(e => !selectedCategory.HasValue || e.Category.CategoryID == selectedCategory);

            return sortOption switch
            {
                "MostPopular" => query.OrderByDescending(e => e.NumberOfParticipants).AsQueryable(),
                "Recent" => query.OrderByDescending(e => e.StartDate).AsQueryable(),
                "Oldest" => query.OrderBy(e => e.StartDate).AsQueryable(),
                "Alphabetical" => query.OrderBy(e => e.Name).AsQueryable(),
                _ => query.OrderBy(e => e.Name).AsQueryable()
            };
        }

        private EventViewModel MapToEventViewModel(Event e) => new()
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
            Category = e.Category,
            Address = e.Address
        };

        private EventViewModel MapToDetailedEventViewModel(Event e, bool hasJoined) => new()
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
            Category = e.Category,
            Address = e.Address,
            HasJoined = hasJoined,
            Comments = e.EventComments.Select(c => new CommentViewModel
            {
                CommentId = c.CommentId,
                Content = c.Content,
                PostedDate = c.PostedDate,
                User = c.User,
                isDeleted = c.isDeleted
            }).ToList()
        };
    }
}
