﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STAREvents.Common.EntityValidationConstants.ApplicationUserConstants;

namespace STAREvents.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.ProfilePictureUrl = DefaultProfilePictureUrl;
        }
        [Required]
        [MaxLength(MaxFirstNameLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(MaxLastNameLength)]
        public string LastName { get; set; } = null!;
        [MaxLength(MaxImgUrlLength)]
        public string ProfilePictureUrl { get; set; }
        [Required]
        public bool isDeleted { get; set; } = false;

        public ICollection<Event> OrganizedEvents { get; set; }
            = new HashSet<Event>();
        public ICollection<UserEventAttendance> UserEventAttendances { get; set; } 
            = new HashSet<UserEventAttendance>();
        public ICollection<Comment> UserComments { get; set; }
            = new HashSet<Comment>();
    }
}
