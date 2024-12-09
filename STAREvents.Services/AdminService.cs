﻿using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Services.Data.Interfaces.STAREvents.Web.Services;
using STAREvents.Web.ViewModels.Admin;
using STAREvents.Web.ViewModels.Events;
using STAREvents.Web.ViewModels.Profile;
using static STAREvents.Common.EntityValidationConstants.RoleNames;
using static STAREvents.Common.ErrorMessagesConstants.EventsServiceErrorMessages;

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

        public async Task<AdminDashboardViewModel> GetAdminDashboardViewModelAsync()
        {
            return new AdminDashboardViewModel
            {
                Events = await GetAllEventsAsync(),
                Users = await GetAllUsersAsync(),
                TotalEvents = await GetTotalEventsAsync(),
                UpcomingEvents = await GetUpcomingEventsCountAsync(),
                PastEvents = await GetPastEventsCountAsync(),
                TotalUsers = await GetTotalUsersAsync()
            };
        }

        public async Task<List<EventViewModel>> GetAllEventsAsync()
        {
            var events = await eventRepository.GetAllAttached().Include(e => e.Organizer).ToListAsync();
            return events.Select(e => new EventViewModel
            {
                EventId = e.EventId,
                Name = e.Name,
                Organizer = e.Organizer,
                isDeleted = e.isDeleted
            }).ToList();
        }

        public async Task<List<ProfileViewModel>> GetAllUsersAsync()
        {
            var users = await userAuthService.GetAllUsersAsync();

            var userViewModels = new List<ProfileViewModel>();

            foreach (var user in users)
            {
                userViewModels.Add(new ProfileViewModel
                {
                    UserId = user.Id.ToString(),
                    Username = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    IsDeleted = user.isDeleted,
                    IsAdmin = await userAuthService.IsUserInRoleAsync(user.Id.ToString(), Administrator)
                });
            }

            return userViewModels;
        }

        public async Task<int> GetTotalEventsAsync()
        {
            var events = await eventRepository.GetAllAsync();
            return events.Count();
        }

        public async Task<int> GetUpcomingEventsCountAsync()
        {
            var events = await eventRepository.GetAllAsync();
            return events.Count(e => e.StartDate > DateTime.Now);
        }

        public async Task<int> GetPastEventsCountAsync()
        {
            var events = await eventRepository.GetAllAsync();
            return events.Count(e => e.EndDate < DateTime.Now);
        }

        public async Task<int> GetTotalUsersAsync()
        {
            var users = await userAuthService.GetAllUsersAsync();
            return users.Count;
        }

        public async Task RecoverEventAsync(Guid id)
        {
            var eventItem = await eventRepository.GetByIdAsync(id);
            if (eventItem != null)
            {
                eventItem.isDeleted = false;
                await eventRepository.UpdateAsync(eventItem);
            }
        }

        public async Task SoftDeleteEventAsync(Guid eventId)
        {
            var eventItem = await eventRepository.GetByIdAsync(eventId);
            if (eventItem != null)
            {
                eventItem.isDeleted = true;
                await eventRepository.UpdateAsync(eventItem);
            }
        }

        public async Task SoftDeleteUserAsync(Guid userId)
        {
            var user = await userAuthService.GetUserByIdAsync(userId.ToString());
            if (user != null)
            {
                user.isDeleted = true;
                await userAuthService.UpdateUserAsync(user);
            }
        }

        public async Task<List<CommentViewModel>> GetEventCommentsAsync(Guid eventId)
        {
            var eventItem = await eventRepository.GetByIdAsync(eventId);
            if (eventItem != null)
            {
                return eventItem.EventComments.Select(c => new CommentViewModel
                {
                    CommentId = c.CommentId,
                    Content = c.Content,
                    User = c.User
                }).ToList();
            }
            return new List<CommentViewModel>();
        }

        public async Task SoftDeleteCommentAsync(Guid commentId, string userName)
        {
            var user = await userAuthService.GetUserByNameAsync(userName);
            if (user == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }

            var eventWithComment = await eventRepository.GetAllAttached().Include(e => e.EventComments)
                .FirstOrDefaultAsync(e => e.EventComments.Any(c => c.CommentId == commentId));
            var isAdmin = await userAuthService.IsUserInRoleAsync(user.Id.ToString(), Administrator);

            if (eventWithComment != null)
            {
                var comment = eventWithComment.EventComments.FirstOrDefault(c => c.CommentId == commentId);
                if (comment != null && (comment.UserId == user.Id || eventWithComment.OrganizerID == user.Id || isAdmin))
                {
                    comment.isDeleted = true;
                    await eventRepository.UpdateAsync(eventWithComment);
                }
            }
        }

        public async Task RecoverUserAsync(Guid userId)
        {
            var user = await userAuthService.GetUserByIdAsync(userId.ToString());
            if (user != null)
            {
                user.isDeleted = false;
                await userAuthService.UpdateUserAsync(user);
            }
        }

        public async Task RemoveAdminRole(Guid userId)
        {
            await userAuthService.RemoveRoleFromUserAsync(userId.ToString(), Administrator);
        }

        public async Task AddAdminRole(Guid userId)
        {
            await userAuthService.AddRoleToUserAsync(userId.ToString(), Administrator);
        }
    }
}
