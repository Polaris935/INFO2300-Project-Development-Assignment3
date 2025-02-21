using Healthcare_And_Wellness.Data;
using Healthcare_And_Wellness.Models;
using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Validations
{
    public class UserNameValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Username is required.");
            }

            var username = value.ToString();
            if (username!.Length < 5 || username.Length > 20)
            {
                return new ValidationResult("Username must be at least 5 characters long and cannot exceed 20 characters.");
            }
            else
            {
                var bookManagementContext = validationContext.GetService<ManagementContext>();
                var userBeingEdited = validationContext.ObjectInstance as User;
                var isUsernameTaken = bookManagementContext.Users.Any(u => u.Username == username && u.Id != userBeingEdited.Id);
                if (isUsernameTaken)
                {
                    return new ValidationResult("This username is already taken. Please choose a different one.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
