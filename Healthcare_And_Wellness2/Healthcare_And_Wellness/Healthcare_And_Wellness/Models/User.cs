using Healthcare_And_Wellness.Validations;
using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [UserNameValidation]
        public string? Username { get; set; }

        [PasswordValidation]
        public string? Password { get; set; }

        [ConfirmPassword]
        public string? ConfirmPassword { get; set; }

        public string? ContentType { get; set; }
        public string? ProfilePic { get; set; }

        [Required(ErrorMessage = "Please enter your name. Do not leave this field empty !")]
        public string? Name { get; set; }

        [AgeValidation]
        public int? Age { get; set; }

        [DateOfBirthValidation]
        public string? DateOfBirth { get; set; }
        public string? Role { get; set; }
    }
}
