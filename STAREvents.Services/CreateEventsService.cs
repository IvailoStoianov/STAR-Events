using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.CreateEvents;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using static STAREvents.Common.EntityValidationConstants.EventConstants;
using static STAREvents.Common.ErrorMessagesConstants.CreateEventsServiceErrorMessages;

namespace STAREvents.Services.Data
{
    public class CreateEventsService : BaseService, ICreateEventsService
    {
        private readonly IRepository<Event, object> eventRepository;
        private readonly IRepository<Category, object> categoryRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CreateEventsService(IRepository<Event, object> _eventRepository, 
            IWebHostEnvironment _webHostEnvironment,
            IRepository<Category, object> _categoryRepository)
        {
            this.eventRepository = _eventRepository;
            this.categoryRepository = _categoryRepository;
            this.webHostEnvironment = _webHostEnvironment;
        }

        public async Task<IEnumerable<Category>> LoadCategoriesAsync()
        {
            return await categoryRepository.GetAllAsync();
        }

        public async Task CreateEventAsync(CreateEventInputModel model, Guid userId)
        {
            if (model.Image != null && model.Image.Length > MaxImageSize)
            {
                throw new InvalidOperationException(string.Format(ImageSizeExceeded, MaxImageSize));
            }

            string imageUrl = string.Empty;
            if (model.Image != null)
            {
                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/event-images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }

                imageUrl = $"/images/event-images/{uniqueFileName}";
            }

            try
            {
                var newEvent = new Event
                {
                    EventId = Guid.NewGuid(),
                    Name = model.Name,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Capacity = model.Capacity,
                    ImageUrl = imageUrl,
                    OrganizerID = userId,
                    CategoryID = model.CategoryId,
                    Address = model.Address,
                    CreatedOnDate = DateTime.UtcNow
                };

                await eventRepository.AddAsync(newEvent);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(EventCreationError, ex);
            }
        }

        
    }
}


