using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Validations
{
    public class AgeValidation: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Age is required.");
            }
            if (int.TryParse(value.ToString(), out int age))
            {
                if (age < 1 || age > 120)
                {
                    return new ValidationResult("Age must be between 1 and 120.");
                }
            }
            else
            {
                return new ValidationResult("Invalid age format.");
            }
            return ValidationResult.Success;
        }
    }
}
