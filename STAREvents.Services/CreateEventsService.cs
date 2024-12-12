using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.ViewModels.CreateEvents;
using static STAREvents.Common.EntityValidationConstants.EventConstants;
using static STAREvents.Common.ErrorMessagesConstants.CreateEventsServiceErrorMessages;
using static STAREvents.Common.FilePathConstants.EventPicturePaths;
using static STAREvents.Common.FilePathConstants.AzureContainerNames;
using Microsoft.Extensions.Configuration;

namespace STAREvents.Services.Data
{
    public class CreateEventsService : BaseService, ICreateEventsService
    {
        private readonly IRepository<Event, object> eventRepository;
        private readonly IRepository<Category, object> categoryRepository;
        private readonly IFileStorageService fileStorageService;
        private readonly bool useAzureBlobStorage;

        public CreateEventsService(
            IRepository<Event, object> eventRepository,
            IRepository<Category, object> categoryRepository,
            IFileStorageService fileStorageService,
            IConfiguration configuration)
        {
            this.eventRepository = eventRepository;
            this.categoryRepository = categoryRepository;
            this.fileStorageService = fileStorageService;
            useAzureBlobStorage = configuration.GetValue<bool>("UseAzureBlobStorage");
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
                if(useAzureBlobStorage)
                {
                    imageUrl = await fileStorageService.UploadFileAsync(model.Image, EventImagesContainer);
                }
                else
                {
                    imageUrl = await fileStorageService.UploadFileAsync(model.Image, DefaultEventPicturePath);
                }
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
    }
}
