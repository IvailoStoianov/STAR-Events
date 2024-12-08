using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces.STAREvents.Web.Services;
using STAREvents.Web.ViewModels.Admin;
using STAREvents.Web.ViewModels.Events;
using STAREvents.Web.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STAREvents.Services.Data
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Event, object> eventRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminService(IRepository<Event, object> eventRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.eventRepository = eventRepository;
            this.userManager = userManager;
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
            var users = await userManager.Users.ToListAsync();

            var userViewModels = new List<ProfileViewModel>();

            foreach (var user in users)
            {
                userViewModels.Add(new ProfileViewModel
                {
                    UserId = user.Id.ToString(),
                    Username = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    IsDeleted = user.isDeleted,
                    IsAdmin = await userManager.IsInRoleAsync(user, "Admin")
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
            var users = await userManager.Users.ToListAsync();
            return users.Count();
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
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                user.isDeleted = true;
                await userManager.UpdateAsync(user);
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

        public async Task DeleteCommentAsync(Guid commentId)
        {
            var eventWithComment = await eventRepository.GetAllAttached().Include(e => e.EventComments).FirstOrDefaultAsync(e => e.EventComments.Any(c => c.CommentId == commentId));

            if (eventWithComment != null)
            {
                var comment = eventWithComment.EventComments.FirstOrDefault(c => c.CommentId == commentId);
                if (comment != null)
                {
                    comment.isDeleted = true;
                    await eventRepository.UpdateAsync(eventWithComment);
                }
            }
        }
        public async Task RecoverUserAsync(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                user.isDeleted = false;
                await userManager.UpdateAsync(user);
            }
        }
        public async Task RemoveAdminRole(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                await userManager.RemoveFromRoleAsync(user, "Admin");
            }
        }

        public async Task AddAdminRole(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
