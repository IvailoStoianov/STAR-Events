using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Common
{
    public static class EntityValidationConstants
    {
        public static class EventConstants
        {
            public const int MaxNameLength = 100;
            public const int MinNameLength = 3;
            public const int MaxDescriptionLength = 1000;
            public const int MinDescriptionLength = 30;
            public const int MaxImgUrlLength = 2048;
            public const string DecimalType = "decimal(18,2)";
        }

        public static class VenueConstants
        {
            public const int MaxNameLength = 100;
            public const int MinNameLength = 3;
            public const int MaxLocationLength = 200;
            public const int MinLocationLength = 5;
        }

        public static class OrganizerConstants
        {
            public const int MaxNameLength = 100;
            public const int MinNameLength = 3;
            public const int MaxContactInfoLength = 200;
        }

        public static class CategoryConstants
        {
            public const int MaxNameLength = 100;
            public const int MinNameLength = 3;
        }
        public static class TicketConstants
        {
            public const int MinQuantity = 1;
            public const int MaxQuantity = 10;
        }
        public static class ApplicationUserConstants
        {
            public const int MaxFirstNameLength = 100;
            public const int MinFirstNameLength = 3;
            public const int MaxLastNameLength = 100;
            public const int MinLastNameLength = 3;
            public const int MaxImgUrlLength = 2048;
            public const int MaxUsernameLength = 50;
            public const int MinUsernameLength = 3;
            public const int MaxPasswordLength = 100;
            public const int MinPasswordLength = 6;
            public const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$";
            public const string PhoneNumberRegex = @"^(\+\d{1,3}[- ]?)?\d{10}$";
        }
        public static class CommentConstants
        {
            public const int MaxContentLength = 1000;
            public const int MinContentLength = 1;
        }
        public static class TagConstants
        {
            public const int MaxTagLength = 50;
            public const int MinTagLength = 1;
        }
    }
}
