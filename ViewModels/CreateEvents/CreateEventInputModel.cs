using Microsoft.AspNetCore.Http;
using STAREvents.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static STAREvents.Common.EntityValidationConstants.DateFormatConstants;
using static STAREvents.Common.ErrorMessagesConstants.CreateEventInputModelErrorMessages;
using static STAREvents.Common.EntityValidationConstants.EventConstants;
using STAREvents.Web.Common.Custom_Attributes;

namespace STAREvents.Web.ViewModels.CreateEvents
{
    public class CreateEventInputModel
    {
        public CreateEventInputModel()
        {
            CreatedOnDate = DateTime.UtcNow;
            StartDate = DateTime.UtcNow;
            EndDate = DateTime.UtcNow;
        }

        [Required(ErrorMessage = EventNameRequired)]
        [MaxLength(MaxNameLength, ErrorMessage = EventNameMaxLength)]
        [MinLength(MinNameLength, ErrorMessage = EventNameMinLength)]
        [Display(Name = "Event Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = DescriptionRequired)]
        [MaxLength(MaxDescriptionLength, ErrorMessage = DescriptionMaxLength)]
        [MinLength(MinDescriptionLength, ErrorMessage = DescriptionMinLength)]
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
        [DateRange("CreatedOnDate", ErrorMessage = StartDateBeforeCreatedOnDate)]
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

        [Required]
        [Display(Name = "Event Image")]
        public IFormFile Image { get; set; } = null!;

        [Required]
        [Display(Name = "Event Category")]
        public Guid CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }
            = new List<Category>();
    }
}
