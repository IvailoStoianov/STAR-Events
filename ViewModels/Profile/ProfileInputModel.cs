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
    public class ProfileInputModel
    {
        [Required(ErrorMessage = RequiredField)]
        [MaxLength(MaxFirstNameLength, ErrorMessage = MaxLength)]
        [MinLength(MinFirstNameLength, ErrorMessage = MinLength)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [MaxLength(MaxLastNameLength, ErrorMessage = MaxLength)]
        [MinLength(MinLastNameLength, ErrorMessage = MinLength)]
        public string LastName { get; set; } = null!;

        [MaxLength(MaxImgUrlLength, ErrorMessage = MaxLength)]
        [Url(ErrorMessage = InvalidUrl)]
        public string ImageUrl { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [MaxLength(MaxUsernameLength, ErrorMessage = MaxLength)]
        [MinLength(MinUsernameLength, ErrorMessage = MinLength)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [EmailAddress(ErrorMessage = InvalidEmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [MinLength(MinPasswordLength, ErrorMessage = MinLength)]
        [MaxLength(MaxPasswordLength, ErrorMessage = MaxLength)]
        [RegularExpression(PasswordRegex, ErrorMessage = PasswordRequirements)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [Compare("Password", ErrorMessage = PasswordMismatch)]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [RegularExpression(PhoneNumberRegex, ErrorMessage = InvalidPhoneNumber)]
        public string PhoneNumber { get; set; } = null!;
    }
}
