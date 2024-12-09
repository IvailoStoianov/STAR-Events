using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Common
{
    public static class ValidationResultConstants
    {

        public const string UknownProperty = "Unknown property: {0}";
        public const string InvalidDate = "Start date must be before end date.";
        public const string InvalidValue = "Invalid value: {0}";
        public const string DateBeforeToday = "The date cannot be before today's date.";
    }
}
