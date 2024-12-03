using Microsoft.AspNetCore.Http;
using STAREvents.Data.Models;
using STAREvents.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STAREvents.Common.EntityValidationConstants.ApplicationUserConstants;
using static STAREvents.Common.ErrorMessagesConstants.ApplicationUserValidationErrorMessages;

namespace STAREvents.Web.ViewModels.Profile
{
    public class ProfileInputModel : IMapFrom<ApplicationUser>
    {
        [Required]
        [MaxLength(MaxFirstNameLength, ErrorMessage = MaxLength)]
        [MinLength(MinFirstNameLength, ErrorMessage = MinLength)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(MaxLastNameLength, ErrorMessage = MaxLength)]
        [MinLength(MinLastNameLength, ErrorMessage = MinLength)]
        public string LastName { get; set; } = string.Empty;

        //[MaxLength(MaxImgUrlLength, ErrorMessage = MaxLength)]
        //[Url(ErrorMessage = InvalidUrl)]
        public byte[] ProfilePicture { get; set; } = Array.Empty<byte>();

        [Required]
        [MaxLength(MaxUsernameLength, ErrorMessage = MaxLength)]
        [MinLength(MinUsernameLength, ErrorMessage = MinLength)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = InvalidEmailAddress)]
        public string Email { get; set; } = string.Empty;
    }
}
