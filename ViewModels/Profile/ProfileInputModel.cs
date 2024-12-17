using Microsoft.AspNetCore.Http;
using STAREvents.Data.Models;
using STAREvents.Services.Mapping;
using System.ComponentModel.DataAnnotations;
using static STAREvents.Common.EntityValidationConstants.ApplicationUserConstants;
using static STAREvents.Common.ErrorMessagesConstants.ApplicationUserValidationErrorMessages;
using static STAREvents.Common.ErrorMessagesConstants.SharedErrorMessages;

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

        public string ProfilePictureUrl { get; set; } = "/images/default-pfp.svg";
        public IFormFile? ProfilePicture { get; set; } 

        [Required]
        [MaxLength(MaxUsernameLength, ErrorMessage = MaxLength)]
        [MinLength(MinUsernameLength, ErrorMessage = MinLength)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = InvalidEmail)]
        public string Email { get; set; } = string.Empty;
    }
}
