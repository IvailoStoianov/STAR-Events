﻿using System;
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
            public const string FailedToUpdateUserProfile = "Failed to update user profile.";
            public const string FailedToChangePassword = "Failed to change password.";
            public const string InvalidImageFormat = "Invalid image format. Only JPEG, PNG, and GIF are allowed.";
            public const string ProfilePictureSizeExceeded = "File size exceeds {0} KB.";
        }
        public static class ProfileControllerErrorMessages
        {
            public const string GeneralErrorForUpdatingProfile = "An error occurred while updating the profile.";
            public const string ProfileLoadError = "An error occurred while loading the profile.";
            public const string EditFormLoadError = "An error occurred while loading the edit form.";
            public const string IncorrectCurrentPasswordError = "The current password is incorrect.";
            public const string UpdateProfileError = "An error occurred while updating the profile.";
        }
    }
}
