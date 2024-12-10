using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.CreateEvents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using static STAREvents.Common.EntityValidationConstants.EventConstants;
using static STAREvents.Common.ErrorMessagesConstants.CreateEventsServiceErrorMessages;
using static STAREvents.Common.FilePathConstants.EventPicturePaths;
using Microsoft.AspNetCore.Http;

namespace STAREvents.Services.Data
{
    public class CreateEventsService : BaseService, ICreateEventsService
    {
        private readonly IRepository<Event, object> eventRepository;
        private readonly IRepository<Category, object> categoryRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CreateEventsService(
            IRepository<Event, object> eventRepository,
            IWebHostEnvironment webHostEnvironment,
            IRepository<Category, object> categoryRepository)
        {
            this.eventRepository = eventRepository;
            this.categoryRepository = categoryRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<Category>> LoadCategoriesAsync()
        {
            return await categoryRepository.GetAllAsync();
        }

        public async Task CreateEventAsync(CreateEventInputModel model, Guid userId)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.Image != null && model.Image.Length > MaxImageSize)
            {
                throw new InvalidOperationException(string.Format(ImageSizeExceeded, MaxImageSize));
            }

            string imageUrl = string.Empty;
            if (model.Image != null)
            {
                imageUrl = await SaveImageAsync(model.Image);
            }

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

            try
            {
                await eventRepository.AddAsync(newEvent);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(EventCreationError, ex);
            }
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, DefaultEventPicturePath);
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            Directory.CreateDirectory(uploadsFolder);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return $"/{DefaultEventPicturePath}/{uniqueFileName}";
        }
    }
}
