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
            ProfilePicture = GetDefaultProfilePicture();
        }
        [Required]
        [MaxLength(MaxFirstNameLength)]
        [MinLength(MinFirstNameLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(MaxLastNameLength)]
        [MinLength(MinLastNameLength)]
        public string LastName { get; set; } = null!;

        public byte[] ProfilePicture { get; set; }

        public ICollection<Event> OrganizedEvents { get; set; }
            = new HashSet<Event>();
        public ICollection<UserEventAttendance> UserEventAttendances { get; set; } 
            = new HashSet<UserEventAttendance>();
        public ICollection<Comment> UserComments { get; set; }
            = new HashSet<Comment>();
        private byte[] GetDefaultProfilePicture()
        {
            var defaultImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/default-pfp.svg");
            return File.ReadAllBytes(defaultImagePath);
        }
    }
}
