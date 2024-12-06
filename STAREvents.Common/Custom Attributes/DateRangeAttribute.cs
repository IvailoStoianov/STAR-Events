using System.ComponentModel.DataAnnotations;
using static STAREvents.Common.ValidationResultConstants;

namespace STAREvents.Web.Common.Custom_Attributes
{
    public class DateRangeAttribute : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public DateRangeAttribute(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(string.Format(InvalidValue, value));
            }

            var endDate = (DateTime)value;
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);

            if (startDateProperty == null)
            {
                return new ValidationResult(string.Format(UknownProperty,_startDatePropertyName));
            }

            var startDate = (DateTime?)startDateProperty.GetValue(validationContext.ObjectInstance);

            if (startDate == null || startDate > endDate)
            {
                return new ValidationResult(InvalidDate);
            }

            return ValidationResult.Success;
        }
    }
}
