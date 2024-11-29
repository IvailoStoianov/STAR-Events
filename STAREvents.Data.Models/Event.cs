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
using Microsoft.EntityFrameworkCore;

//TODO Add [Comment]

namespace STAREvents.Data.Models
{
    public class Event
    {
        public Event()
        {
            EventId = Guid.NewGuid();
        }
        [Key]
        public Guid EventId { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        [MinLength(MinNameLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(MaxDescriptionLength)]
        [MinLength(MinDescriptionLength)]
        public string Description { get; set; } = null!;
        [MaxLength(MaxImgUrlLength)]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedOnDate { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        [Comment("This represents how many users have joined the event.")]
        public int NumberOfParticipants { get; set; }
        [Comment("Shows wether the event has been deleted")]
        public bool isDeleted { get; set; }

        public Guid OrganizerID { get; set; }

        [ForeignKey("OrganizerID")]
        public ApplicationUser Organizer { get; set; } = null!;


        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; } = null!;

        public ICollection<EventCategory> EventCategories { get; set; }
            = new HashSet<EventCategory>();
        public ICollection<Comment> EventComments { get; set; }
            = new HashSet<Comment>();
        public ICollection<Tag> Tags { get; set; } 
            = new HashSet<Tag>();
        public ICollection<UserEventAttendance> UserEventAttendances { get; set; } 
            = new HashSet<UserEventAttendance>();

    }
}
