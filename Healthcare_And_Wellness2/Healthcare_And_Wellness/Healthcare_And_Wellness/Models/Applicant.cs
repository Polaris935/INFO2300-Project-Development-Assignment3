using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Models
{
    public class Applicant
    {
        [Key]
        public int userID { get; set; }
        public string? nameUser { get; set; }
        public string? emailUser { get; set; }

        public string? statusUser { get; set; }  //Message Not Sent/Accepted/Denied

        public Job? Job { get; set; }

        public int jobID { get; set; }
    }
}
