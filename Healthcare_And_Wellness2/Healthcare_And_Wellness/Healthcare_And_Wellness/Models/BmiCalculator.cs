using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Models
{
    public class BmiCalculator
    {
        [Required(ErrorMessage = "Please enter the height")]
        [Range(1, 10, ErrorMessage = "Please enter between 1 and 10")]
        // ? will make them pick default value in this
        public double? Height { get; set; }

        [Required(ErrorMessage = "Please enter the weight")]
        [Range(1, 5, ErrorMessage = "Please enter between 1 and 5")]
        public double? Weight { get; set; }
        public double? Result { get; set; }

        public double? Calculate()
        {
            return Weight / (Height * Height);
        }
    }
}
