using Healthcare_And_Wellness.Models;
using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Validations
{
    public class DateOfBirthValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var user = (User)validationContext.ObjectInstance;
            var dateOfBirth = value?.ToString();

            if (string.IsNullOrEmpty(dateOfBirth))
            {
                return new ValidationResult("Date of Birth is required.");
            }
            else
            {
                if (dateOfBirth.Length != 10 || dateOfBirth[4] != '-' || dateOfBirth[7] != '-')
                {
                    return new ValidationResult("Invalid Date of Birth format.");
                }
                else
                {
                    if (int.TryParse(dateOfBirth.Substring(0, 4), out int year) && int.TryParse(dateOfBirth.Substring(5, 2), out int month) && int.TryParse(dateOfBirth.Substring(8, 2), out int day))
                    {
                        int currentYear = 2024;
                        int currentMonth = 11;
                        int currentDay = 25;
                        int calculatedAge = currentYear - year;
                        if (month > currentMonth || (month == currentMonth && day > currentDay))
                        {
                            calculatedAge--;
                        }

                        if (calculatedAge != user.Age)
                        {
                            return new ValidationResult("Date of Birth does not match the Age.");
                        }
                        else
                        {
                            return ValidationResult.Success;
                        }
                    }
                    else
                    {
                        return new ValidationResult("Invalid Date of Birth format.");
                    }
                }
            }
        }
    }
}
