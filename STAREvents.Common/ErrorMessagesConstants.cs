using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Common
{
    public static class ErrorMessagesConstants
    {
        public static class DataMessages
        {
            public const string SeedFileNotFound = "Seed file not found at: {0}";
        }
        public static class ApplicationUserValidationErrorMessages
        {
            public const string RequiredField = "This field is required";
            public const string MaxLength = "The field cannot exceed {1} characters";
            public const string MinLength = "The field must be at least {1} characters long";
            public const string InvalidEmailAddress = "Invalid email address";
            public const string InvalidUrl = "Invalid URL format";
            public const string PasswordRequirements = "Password must have at least one uppercase letter, one lowercase letter, one digit, and one special character";
            public const string PasswordMismatch = "Passwords do not match";
            public const string InvalidPhoneNumber = "Invalid phone number. Example: +1234567890 or 1234567890";
        }
        public static class ProfileServiceErrorMessages
        {
            public const string UserNotFound = "User not found.";
        }
        public static class ProfileControllerErrorMessages
        {
            public const string ProfileLoadError = "An error occurred while loading the profile.";
            public const string EditFormLoadError = "An error occurred while loading the edit form.";
        }
    }
}
