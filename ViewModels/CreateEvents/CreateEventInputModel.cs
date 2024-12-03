using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Web.ViewModels.CreateEvents
{
    public class CreateEventInputModel
    {
        [Required(ErrorMessage = "Event name is required.")]
        [MaxLength(100, ErrorMessage = "Event name cannot exceed 100 characters.")]
        [Display(Name = "Event Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Capacity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }

        [Display(Name = "Event Image")]
        public byte[] Image { get; set; } = Array.Empty<byte>();
    }
}
