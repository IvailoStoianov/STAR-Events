using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static STAREvents.Common.EntityValidationConstants.EventConstants;

namespace STAREvents.Data.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        [MinLength(MinNameLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(MaxDescriptionLength)]
        [MinLength(MinDescriptionLength)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        public int VenueID { get; set; }

        [ForeignKey("VenueID")]
        public Venue Venue { get; set; } = null!;

        public int OrganizerID { get; set; }

        [ForeignKey("OrganizerID")]
        public Organizer Organizer { get; set; } = null!;

        [Required]
        [Column(TypeName = DecimalType)]
        public decimal TicketPrice { get; set; }

        [Required]
        public int MaxAttendees { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; } = null!;

        public ICollection<EventCategory> EventCategories { get; set; }
            = new HashSet<EventCategory>();
        public ICollection<Ticket> Tickets { get; set; }
            = new HashSet<Ticket>();
    }
}
