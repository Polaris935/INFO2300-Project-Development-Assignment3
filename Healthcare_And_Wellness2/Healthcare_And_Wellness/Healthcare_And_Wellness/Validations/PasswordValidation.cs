using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Validations
{
    public class PasswordValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Password is required.");
            }
            else
            {
                var password = value.ToString();
                if (string.IsNullOrEmpty(password))
                {
                    return new ValidationResult("Please enter password.");
                }
                else
                {
                    if (password.Length < 8)
                    {
                        return new ValidationResult("Password must be at least 8 characters long.");
                    }
                    else
                    {
                        bool containsUpperCase = false;
                        bool containsDigit = false;
                        bool containsSpecialCharacter = false;

                        foreach (char c in password)
                        {
                            if (char.IsUpper(c))
                            {
                                containsUpperCase = true;
                            }
                            else if (char.IsDigit(c))
                            {
                                containsDigit = true;
                            }
                            else if (!char.IsLetterOrDigit(c))
                            {
                                containsSpecialCharacter = true;
                            }
                        }
                        if (!containsUpperCase)
                        {
                            return new ValidationResult("Password must contain at least one uppercase letter.");
                        }
                        else
                        {
                            if (!containsDigit)
                            {
                                return new ValidationResult("Password must contain at least one number.");
                            }
                            else
                            {
                                if (!containsSpecialCharacter)
                                {
                                    return new ValidationResult("Password must contain at least one special character.");
                                }
                                else
                                {
                                    return ValidationResult.Success;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
