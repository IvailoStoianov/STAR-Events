using Microsoft.AspNetCore.Http;
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

namespace STAREvents.Services.Data
{
    public class MyEventsService : EventHelperService, IMyEventsService
    {
        private readonly IRepository<Event, object> eventRepository;
        private readonly IRepository<Category, object> categoryRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public MyEventsService(IRepository<Event, object> eventRepository,
                               IRepository<Category, object> categoryRepository,
                               IRepository<UserEventAttendance, object> attendanceRepository,
                               UserManager<ApplicationUser> userManager)
            : base(eventRepository, attendanceRepository, userManager)
        {
            this.eventRepository = eventRepository;
            this.categoryRepository = categoryRepository;
            this.userManager = userManager;
        }

        public async Task<EventsViewModel> LoadMyEventsAsync(string searchTerm, Guid? selectedCategory, string sortOption, string userId, int page = 1, int pageSize = 12)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var events = await eventRepository.GetAllAttached()
                                              .Include(e => e.Organizer)
                                              .Include(e => e.Category)
                                              .Include(e => e.UserEventAttendances)
                                              .Where(e => e.isDeleted == false)
                                              .Where(e => e.Organizer.Id == user.Id || e.UserEventAttendances.Any(uea => uea.UserId == user.Id))
                                              .ToListAsync();

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
                Category = e.Category,
                Address = e.Address,
                HasJoined = e.UserEventAttendances.Any(uea => uea.UserId == user.Id)
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
    }
}




