using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STAREvents.Common.EntityValidationConstants.VenueConstants;

namespace STAREvents.Data.Models
{
    public class Venue
    {
        [Key]
        public int VenueID { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        [MinLength(MinNameLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(MaxLocationLength)]
        [MinLength(MinLocationLength)]
        public string Location { get; set; } = null!;

        [Required]
        public int Capacity { get; set; }

        public ICollection<Event> Events { get; set; }
            = new HashSet<Event>();
    }
}
