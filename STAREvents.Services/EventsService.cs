using Microsoft.EntityFrameworkCore.Diagnostics;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Services.Data
{
    public class EventsService : IEventsService
    {
        private IRepository<Event, object> eventRepository;
        private IRepository<Category, object> categoryRepository;
        public EventsService(IRepository<Event, object> _eventRepository,
            IRepository<Category, object> categoryRepository)
        {
            this.eventRepository = _eventRepository;
            this.categoryRepository = categoryRepository;
        }
        public async Task<EventsViewModel> LoadEventAsync(string searchTerm, Guid? selectedCategory, string sortOption, int page = 1, int pageSize = 12)
        {
            var events = await eventRepository.GetAllAsync();
            var categories = await categoryRepository.GetAllAsync();

            var filteredEvents = events
                .Where(e => string.IsNullOrEmpty(searchTerm) || e.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .Where(e => !selectedCategory.HasValue || e.Category.CategoryID == selectedCategory);

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
    }
}
