using Microsoft.AspNetCore.Identity;
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
            Id = Guid.NewGuid();
        }
        [Required]
        [MaxLength(MaxFirstNameLength)]
        [MinLength(MinFirstNameLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(MaxLastNameLength)]
        [MinLength(MinLastNameLength)]
        public string LastName { get; set; } = null!;
        [MaxLength(MaxImgUrlLength)]
        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<Ticket> Tickets { get; set; }
            = new HashSet<Ticket>();
        public ICollection<Event> OrganizedEvents { get; set; }
            = new HashSet<Event>();
    }
}
