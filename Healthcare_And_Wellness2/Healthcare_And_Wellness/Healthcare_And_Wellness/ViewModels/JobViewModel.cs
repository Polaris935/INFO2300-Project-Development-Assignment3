using Healthcare_And_Wellness.Models;

namespace Healthcare_And_Wellness.ViewModels
{
    public class JobViewModel
    {
        public Job? Job { get; set; }
        public List<Applicant>? applicants { get; set; }
    }
}
