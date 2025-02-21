using Healthcare_And_Wellness.Models;
using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Validations
{
    public class ConfirmPassword: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var user = (User)validationContext.ObjectInstance;
            var confirmPassword = value?.ToString();
            var password = user.Password;

            if (string.IsNullOrEmpty(confirmPassword))
            {
                return new ValidationResult("Confirm Password field is required. Please enter same characters you enetered in Password field");
            }

            if (password != confirmPassword)
            {
                return new ValidationResult("Passwords don't match. Please check your Password");
            }

            return ValidationResult.Success;
        }
    }
}
