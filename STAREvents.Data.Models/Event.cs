﻿using Microsoft.VisualBasic;
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
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; } = null!;
        [Required]
        public string ImageUrl { get; set; } = null!;
        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;
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

        public Guid CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; } = null!;

        public ICollection<EventCategory> EventCategories { get; set; }
            = new HashSet<EventCategory>();
        public ICollection<Comment> EventComments { get; set; }
            = new HashSet<Comment>();
        public ICollection<UserEventAttendance> UserEventAttendances { get; set; } 
            = new HashSet<UserEventAttendance>();
        
    }
}
