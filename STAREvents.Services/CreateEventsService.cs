using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.CreateEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Services.Data
{
    public class CreateEventsService : ICreateEventsService
    {
        private readonly IRepository<Event, object> repository;
        public CreateEventsService(IRepository<Event, object> _repository)
        {
            this.repository = _repository;
        }

        public async Task CreateEventAsync(CreateEventInputModel model)
        {
            var newEvent = new Event
            {
                EventId = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Capacity = model.Capacity,
                ImageUrl = model.Image != null ? Convert.ToBase64String(model.Image) : null,
                CreatedOnDate = DateTime.UtcNow
            };

            await repository.AddAsync(newEvent);
        }
    }
}
