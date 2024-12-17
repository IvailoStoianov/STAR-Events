using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Admin;
using STAREvents.Web.ViewModels.Events;
using STAREvents.Web.ViewModels.Profile;
using STAREvents.Common;
using static STAREvents.Common.EntityValidationConstants.RoleNames;
using static STAREvents.Common.ErrorMessagesConstants.EventsServiceErrorMessages;
using static STAREvents.Common.ErrorMessagesConstants.AdminServiceErrorMessages;

namespace STAREvents.Services.Data
{
    public class AdminService : BaseService, IAdminService
    {
        private readonly IRepository<Event, object> eventRepository;
        private readonly IUserAuthService userAuthService;

        public AdminService(IRepository<Event, object> eventRepository, IUserAuthService userAuthService)
        {
            this.eventRepository = eventRepository;
            this.userAuthService = userAuthService;
        }

        public async Task<ServiceResult<AdminDashboardViewModel>> GetAdminDashboardViewModelAsync()
        {
            var eventsResult = await GetAllEventsAsync();
            var usersResult = await GetAllUsersAsync();

            if (!eventsResult.Succeeded || !usersResult.Succeeded)
            {
                return ServiceResult<AdminDashboardViewModel>.Failure(FailedToLoadDashboardData);
            }

            var events = eventsResult.Data;
            var users = usersResult.Data;

            var dashboardViewModel = new AdminDashboardViewModel
            {
                Events = events,
                Users = users,
                TotalEvents = events.Count,
                UpcomingEvents = events.Count(e => e.StartDate > DateTime.Now),
                PastEvents = events.Count(e => e.EndDate < DateTime.Now),
                TotalUsers = users.Count
            };

            return ServiceResult<AdminDashboardViewModel>.Success(dashboardViewModel);
        }

        public async Task<ServiceResult<List<EventViewModel>>> GetAllEventsAsync()
        {
            var events = await eventRepository.GetAllAttached().Include(e => e.Organizer).ToListAsync();

            var result = events.Select(e => new EventViewModel
            {
                EventId = e.EventId,
                Name = e.Name,
                Organizer = e.Organizer,
                isDeleted = e.isDeleted
            }).ToList();

            return ServiceResult<List<EventViewModel>>.Success(result);
        }

        public async Task<ServiceResult<List<ProfileViewModel>>> GetAllUsersAsync()
        {
            var users = await userAuthService.GetAllUsersAsync();

            var userViewModels = new List<ProfileViewModel>();

            foreach (var user in users)
            {
                var isAdmin = await userAuthService.IsUserInRoleAsync(user.Id.ToString(), Administrator);
                userViewModels.Add(new ProfileViewModel
                {
                    UserId = user.Id.ToString(),
                    Username = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    IsDeleted = user.isDeleted,
                    IsAdmin = isAdmin
                });
            }

            return ServiceResult<List<ProfileViewModel>>.Success(userViewModels);
        }

        public async Task<ServiceResult> SoftDeleteEventAsync(Guid eventId)
        {
            var eventItem = await eventRepository.GetByIdAsync(eventId);
            if (eventItem == null)
                return ServiceResult.Failure(EventNotFound);

            eventItem.isDeleted = true;
            await eventRepository.UpdateAsync(eventItem);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RecoverEventAsync(Guid id)
        {
            var eventItem = await eventRepository.GetByIdAsync(id);
            if (eventItem == null)
                return ServiceResult.Failure(EventNotFound);

            eventItem.isDeleted = false;
            await eventRepository.UpdateAsync(eventItem);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> SoftDeleteUserAsync(Guid userId)
        {
            var user = await userAuthService.GetUserByIdAsync(userId.ToString());
            if (user == null)
                return ServiceResult.Failure(UserNotFound);

            user.isDeleted = true;
            await userAuthService.UpdateUserAsync(user);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RecoverUserAsync(Guid userId)
        {
            var user = await userAuthService.GetUserByIdAsync(userId.ToString());
            if (user == null)
                return ServiceResult.Failure(UserNotFound);

            user.isDeleted = false;
            await userAuthService.UpdateUserAsync(user);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<CommentViewModel>>> GetEventCommentsAsync(Guid eventId)
        {
            var eventItem = await eventRepository.GetByIdAsync(eventId);
            if (eventItem == null)
                return ServiceResult<List<CommentViewModel>>.Failure(EventNotFound);

            var comments = eventItem.EventComments.Select(c => new CommentViewModel
            {
                CommentId = c.CommentId,
                Content = c.Content,
                User = c.User
            }).ToList();

            return ServiceResult<List<CommentViewModel>>.Success(comments);
        }

        public async Task<ServiceResult> SoftDeleteCommentAsync(Guid commentId, string userName)
        {
            var user = await userAuthService.GetUserByNameAsync(userName);
            if (user == null)
                return ServiceResult.Failure(UserNotFound);

            var eventWithComment = await eventRepository.GetAllAttached()
                .Include(e => e.EventComments)
                .FirstOrDefaultAsync(e => e.EventComments.Any(c => c.CommentId == commentId));

            if (eventWithComment == null)
                return ServiceResult.Failure(EventNotFound);

            var comment = eventWithComment.EventComments.FirstOrDefault(c => c.CommentId == commentId);
            var isAdmin = await userAuthService.IsUserInRoleAsync(user.Id.ToString(), Administrator);

            if (comment != null && (comment.UserId == user.Id || eventWithComment.OrganizerID == user.Id || isAdmin))
            {
                comment.isDeleted = true;
                await eventRepository.UpdateAsync(eventWithComment);
                return ServiceResult.Success();
            }

            return ServiceResult.Failure(NotAllowedToDeleteComment);
        }

        public async Task<ServiceResult> AddAdminRole(Guid userId)
        {
            await userAuthService.AddRoleToUserAsync(userId.ToString(), Administrator);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RemoveAdminRole(Guid userId)
        {
            await userAuthService.RemoveRoleFromUserAsync(userId.ToString(), Administrator);
            return ServiceResult.Success();
        }
    }
}
