using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;

using STAREvents.Data.Models;
using static STAREvents.Common.EntityValidationConstants.DateFormatConstants;
using static STAREvents.Common.ErrorMessagesConstants.CreateEventInputModelErrorMessages;
using static STAREvents.Common.EntityValidationConstants.EventConstants;
using STAREvents.Web.Common.Custom_Attributes;

namespace STAREvents.Web.ViewModels.Events
{
    public class EditEventInputModel
    {
        [Required]
        public Guid EventId { get; set; }
        [Required(ErrorMessage = EventNameRequired)]
        [MaxLength(100, ErrorMessage = EventNameMaxLength)]
        [Display(Name = "Event Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = DescriptionRequired)]
        [MaxLength(1000, ErrorMessage = DescriptionMaxLength)]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        [Required]
        [Display(Name = "Address")]
        [MaxLength(AddressMaxLength)]
        [MinLength(AddressMinLength)]
        public string Address { get; set; } = null!;

        [Required]
        [DisplayFormat(DataFormatString = "{0:" + EventDateTimeFormat + "}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime CreatedOnDate { get; set; }

        [Required(ErrorMessage = StartDateRequired)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:" + EventDateTimeFormat + "}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = EndDateRequired)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:" + EventDateTimeFormat + "}", ApplyFormatInEditMode = true)]
        [DateRange("StartDate", ErrorMessage = EndDateBeforeStartDate)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = CapacityRequired)]
        [Range(MinCapacity, MaxCapacity, ErrorMessage = CapacityRange)]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }

        [Display(Name = "Event Image")]
        public IFormFile? Image { get; set; }

        [Required]
        [Display(Name = "Event Category")]
        public Guid CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }
            = new List<Category>();
    }
}
