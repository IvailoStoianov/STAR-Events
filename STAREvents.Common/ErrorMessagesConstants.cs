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
            public const string FailedToUpdateUserProfile = "Failed to update user profile.";
            public const string FailedToChangePassword = "Failed to change password.";
            public const string InvalidImageFormat = "Invalid image format. Only JPEG, PNG, and SVG are allowed.";
            public const string ProfilePictureSizeExceeded = "File size exceeds {0} KB.";
            public const string PasswordsAreRequired = "Current password and new password must be provided.";
            public const string AllFieldsAreRequired = "All fields are required.";
            public const string InvalidEmail = "Invalid email address.";
            public const string FaildSoftDeleteUser = "Failed to soft delete user.";
        }
        public static class ProfileControllerErrorMessages
        {
            public const string GeneralErrorForUpdatingProfile = "An error occurred while updating the profile.";
            public const string ProfileLoadError = "An error occurred while loading the profile.";
            public const string EditFormLoadError = "An error occurred while loading the edit form.";
            public const string IncorrectCurrentPasswordError = "The current password is incorrect.";
            public const string UpdateProfileError = "An error occurred while updating the profile.";
        }
        public static class CreateEventInputModelErrorMessages
        {
            public const string EventNameRequired = "Event name is required.";
            public const string EventNameMaxLength = "Event name cannot exceed 100 characters.";
            public const string EventNameMinLength = "Event name must be at least 3 characters long.";
            public const string DescriptionRequired = "Description is required.";
            public const string DescriptionMaxLength = "Description cannot exceed 1000 characters.";
            public const string DescriptionMinLength = "Description must be at least 30 characters long.";
            public const string StartDateRequired = "Start date is required.";
            public const string EndDateRequired = "End date is required.";
            public const string EndDateBeforeStartDate = "End date must be after start date.";
            public const string StartDateBeforeCreatedOnDate = "Start date must be after created on date.";
            public const string CapacityRequired = "Capacity is required.";
            public const string CapacityRange = "Capacity must be at least 1.";
        }
        public static class EventsServiceErrorMessages
        {
            public const string EventNotFound = "Event not found.";
            public const string UserNotFound = "User not found.";
            public const string ImageRequired = "Image is required.";
            public const string ImageSizeExceeded = "File size exceeds {0} MB.";
            public const string EventCreationError = "An error occurred while creating the event.";
            public const string CommentNotFound = "Comment not found.";
            public const string UnauthorizedCommentDeletion = "You are not authorized to delete this comment.";
            public const string UserNotJoinedEvent = "User has not joined the event.";
            public const string EventFull = "Event is already full.";
        }
        public static class ImageRelatedErrorMessages
        {
            public const string InvalidImageFormat = "Invalid image format. Only JPG, JPEG, PNG, and SVG are allowed.";
            public const string ImageSizeExceeded = "File size exceeds {0} MB.";
        }
        public static class LoginErrorMessages
        {
            public const string ProfileDeleted = "This profile has been deleted.";
            public const string InvalidLoginAttempt = "Invalid login attempt.";
            public const string UserIsLoggedIn = "User logged in.";
            public const string InvalidLogInAttempt = "Invalid login attempt.";
        }
        public static class UserAuthServiceMessages
        {
            public const string UserNotFound = "User not found.";
            public const string UserAlreadyInRole = "User is already in this role.";
            public const string UserIsNotInRole = "User is not in this role.";
            public const string FailedToChangePassword = "Failed to change password.";
            public const string FailedToSoftDeleteUser = "Failed to soft delete user.";
            public const string AllFieldsAreRequired = "All fields are required.";
            public const string InvalidEmail = "Invalid email address.";
        }
        public static class AdminServiceErrorMessages
        {
            public const string FailedToLoadDashboardData = "Failed to load dashboard data.";
            public const string NotAllowedToDeleteComment = "You are not authorized to delete this comment.";
        }
        public static class AdminControllerErrorMessages
        {
            public const string FailedToLoadDashboardData = "Failed to load dashboard data.";
            public const string FailedToDeleteEvent = "Failed to delete event.";
            public const string FailedToRecoverEvent = "Failed to recover event.";
            public const string FailedToDeleteUser = "Failed to delete user.";
            public const string FailedToRecoverUser = "Failed to recover user.";
            public const string FailedToGrantAdminRole = "Failed to grant admin role.";
            public const string FailedToRemoveAdminRole = "Failed to remove admin role.";
        }
        public static class NotificationsServiceErrorMessages
        {
            public const string NotificationNotFound = "Notification not found.";
            public const string NotificationAlreadyMarked = "Notification is already marked as read.";
        }
        public static class NotifcationsControllerErrorMessages
        {
            public const string FailedToLoadNotifications = "Failed to load notifications.";
            public const string FailedToMarkNotifcationAsRead = "Failed to mark notification as read.";
        }
    }
}
