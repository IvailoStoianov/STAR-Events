using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STAREvents.Common.EntityValidationConstants.OrganizerConstants;

namespace STAREvents.Data.Models
{
    public class Organizer
    {
        [Key]
        public int OrganizerID { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        [MinLength(MinNameLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(MaxContactInfoLength)]
        public string ContactInfo { get; set; } = null!;

        public ICollection<Event> Events { get; set; } 
            = new HashSet<Event>();
    }
}
