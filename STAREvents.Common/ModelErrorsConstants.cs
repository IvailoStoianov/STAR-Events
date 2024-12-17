using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace STAREvents.Common
{
    public static class ModelErrorsConstants
    {
        public static class Password
        {
            public const string PasswordRequired = "Password is required.";
            public const string PasswordMinLength = "Password must be at least {0} characters long.";
            public const string PasswordRequireDigit = "Password must contain at least one digit.";
            public const string PasswordRequireLowercase = "Password must contain at least one lowercase letter.";
            public const string PasswordRequireUppercase = "Password must contain at least one uppercase letter.";
            public const string PasswordRequireNonAlphanumeric = "Password must contain at least one non-alphanumeric character.";
            public const string PasswordsDontMatch = "The confirm password does not match the new password.";
        }
        public static class Date
        {
            public const string StartDateBeforeEndDate = "Start date must be earlier than the end date.";
        }
    }
}
