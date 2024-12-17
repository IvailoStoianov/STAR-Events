using Microsoft.Extensions.Configuration;
using STAREvents.Common;
using System.ComponentModel.DataAnnotations;
using static STAREvents.Common.ModelErrorsConstants.Password;   

public class CustomPasswordValidationAttribute : ValidationAttribute
{
    private readonly IConfiguration _configuration;

    public CustomPasswordValidationAttribute()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var password = value as string;
        if (string.IsNullOrEmpty(password))
        {
            return new ValidationResult(PasswordRequired);
        }

        var requireDigit = _configuration.GetValue<bool>("Identity:Password:RequireDigits");
        var requireLowercase = _configuration.GetValue<bool>("Identity:Password:RequireLowercase");
        var requireUppercase = _configuration.GetValue<bool>("Identity:Password:RequireUppercase");
        var requireNonAlphanumeric = _configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumerical");
        var requiredLength = _configuration.GetValue<int>("Identity:Password:RequiredLength");

        if (password.Length < requiredLength)
        {
            return new ValidationResult(string.Format(PasswordMinLength, requiredLength));
        }

        if (requireDigit && !password.Any(char.IsDigit))
        {
            return new ValidationResult(PasswordRequireDigit);
        }

        if (requireLowercase && !password.Any(char.IsLower))
        {
            return new ValidationResult(PasswordRequireLowercase);
        }

        if (requireUppercase && !password.Any(char.IsUpper))
        {
            return new ValidationResult(PasswordRequireUppercase);
        }

        if (requireNonAlphanumeric && !password.Any(ch => !char.IsLetterOrDigit(ch)))
        {
            return new ValidationResult(PasswordRequireNonAlphanumeric);
        }

        return ValidationResult.Success;
    }
}
