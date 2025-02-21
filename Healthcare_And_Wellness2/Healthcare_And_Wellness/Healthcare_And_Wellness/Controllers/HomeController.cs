using Healthcare_And_Wellness.Data;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_And_Wellness.Controllers
{
    public class HomeController : Controller
    {
        private readonly ManagementContext _context;

        public HomeController(ManagementContext context)
        {
            _context = context;
        }

        private void PopulateViewBag()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Index()
        {
            PopulateViewBag();
            ViewBag.Username = HttpContext.Session.GetString("Username");
            string userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Account");
            }
            int userId = int.Parse(userIdString);
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}
