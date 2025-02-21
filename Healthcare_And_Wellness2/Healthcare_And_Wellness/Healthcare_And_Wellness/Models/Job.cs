namespace Healthcare_And_Wellness.Models
{
    public class Job
    {
        public int jobID { get; set; }
        public string? jobName { get; set; }

        public ICollection<Applicant>? applicants { get; set; }

        public string? statusJob { get; set; }

        public string? description { get; set; }
    }
}
