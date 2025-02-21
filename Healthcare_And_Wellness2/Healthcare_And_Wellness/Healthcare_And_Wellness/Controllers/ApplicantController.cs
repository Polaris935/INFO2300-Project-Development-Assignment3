using Healthcare_And_Wellness.Data;
using Healthcare_And_Wellness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_And_Wellness.Controllers
{
    public class ApplicantController : Controller
    {
        private ManagementContext _managementContext;

        public ApplicantController(ManagementContext managementContext)
        {
            _managementContext = managementContext;
        }

        private void PopulateViewBag()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
        }

        public IActionResult ListJobs()
        {
            PopulateViewBag();
            List<Job> jobs = _managementContext.jobs.ToList();
            return View(jobs);
        }

        [HttpGet]
        public IActionResult AddUser(int id)
        {
            PopulateViewBag();
            return View(new Applicant() { jobID = id });
        }

        [HttpPost]
        public IActionResult AddUser(Applicant user)
        {
            //PopulateViewBag();
            //if (ModelState.IsValid)
            //{
            //    _managementContext.applicants.Add(user);
            //    _managementContext.SaveChanges();

            //    var job = _managementContext.jobs.Find(user.jobID);
            //    if (job != null)
            //    {
            //        job.statusJob = "Applied";
            //        _managementContext.jobs.Update(job);
            //        _managementContext.SaveChanges();
            //    }

            //    return RedirectToAction("ListJobs", "Applicant");
            //}
            //return View(user);

            PopulateViewBag();
            if (ModelState.IsValid)
            {
                // Simply add the new applicant without checking if they already applied
                _managementContext.applicants.Add(user);
                _managementContext.SaveChanges();

                // Optionally, if you want to track if a job has any applicants or keep it as "Apply"
                // you could leave the status of the job unchanged or set it back to "Apply" each time a new applicant applies.
                var job = _managementContext.jobs.Find(user.jobID);
                if (job != null)
                {
                    // If you want to allow applications continuously, we don’t change the job status to "Applied"
                    // Just ensure the job is still available for more applicants
                    if (job.statusJob != "Apply")
                    {
                        job.statusJob = "Apply";  // Keeps it open for more applications
                        _managementContext.jobs.Update(job);
                        _managementContext.SaveChanges();
                    }
                }

                // Redirect back to the job list to continue applying
                return RedirectToAction("ListJobs", "Applicant");
            }
            return View(user);
        }
    }
}
