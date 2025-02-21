using Healthcare_And_Wellness.Data;
using Healthcare_And_Wellness.Models;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_And_Wellness.Controllers
{
    public class AccountController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ManagementContext _context;

        public AccountController(ManagementContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        private void PopulateViewBag()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
        }

        [HttpGet]
        public IActionResult Register(int? id = null)
        {
            if (id == null)
            {
                return View(new User());
            }
            var user = _context.Users.Find(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult EditProfile()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                TempData["SessionTimeoutMessage"] = "Your session has expired. Please log in again.";
                return RedirectToAction("Login");
            }
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            return RedirectToAction("Register", new { id = userId });
        }

        [HttpPost]
        public IActionResult Register(User user, IFormFile? profilePic, bool RemoveProfilePic = false)
        {
            if (ModelState.IsValid)
            {
                if (RemoveProfilePic)
                {
                    if (!string.IsNullOrEmpty(user.ProfilePic))
                    {
                        string existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, user.ProfilePic);
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }
                    }
                    user.ProfilePic = "ProfilePics/default_user.png";
                    user.ContentType = "image/png";
                }
                else if (profilePic != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ProfilePics");
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }
                    string filePath = Path.Combine(uploadFolder, profilePic.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        profilePic.CopyTo(fileStream);
                    }
                    user.ProfilePic = Path.Combine("ProfilePics", profilePic.FileName);
                    user.ContentType = profilePic.ContentType;
                }
                if (user.Id == 0)
                {
                    if (_context.Users.Any(u => u.Username == user.Username))
                    {
                        ModelState.AddModelError("", "Username already exists");
                        return View(user);
                    }
                    user.Role = "Member";
                    if (string.IsNullOrEmpty(user.ProfilePic))
                    {
                        user.ProfilePic = "ProfilePics/default_user.png";
                        user.ContentType = "image/png";
                    }
                    _context.Users.Add(user);
                }
                else
                {
                    var existingUser = _context.Users.Find(user.Id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }
                    existingUser.Name = user.Name;
                    existingUser.Age = user.Age;
                    existingUser.DateOfBirth = user.DateOfBirth;
                    existingUser.Username = user.Username;
                    existingUser.Password = user.Password;
                    existingUser.ConfirmPassword = user.ConfirmPassword;
                    if (profilePic != null)
                    {
                        existingUser.ProfilePic = user.ProfilePic;
                        existingUser.ContentType = user.ContentType;
                    }
                    else if (RemoveProfilePic)
                    {
                        existingUser.ProfilePic = "ProfilePics/default_user.png";
                        existingUser.ContentType = "image/png";
                    }
                    else if (string.IsNullOrEmpty(existingUser.ProfilePic))
                    {
                        existingUser.ProfilePic = "ProfilePics/default_user.png";
                        existingUser.ContentType = "image/png";
                    }
                    _context.Update(existingUser);
                    if (HttpContext.Session.GetString("UserId") == user.Id.ToString())
                    {
                        HttpContext.Session.SetString("Username", existingUser.Username);
                        HttpContext.Session.SetString("ProfilePic", existingUser.ProfilePic);
                    }
                }
                _context.SaveChanges();
                if (user.Id == 0)
                {
                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetString("ProfilePic", user.ProfilePic);
                    return RedirectToAction("Login");
                }
                TempData["SuccessMessage"] = "Profile updated successfully.";
                return RedirectToAction("Profile");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);
            ViewBag.Username = user.Username;
            ViewBag.Role = user.Role;

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Home", "Home");
        }

        public IActionResult Profile()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            if (HttpContext.Session.GetString("UserId") == null)
            {
                TempData["SessionTimeoutMessage"] = "Your session has expired. Please log in again.";
                return RedirectToAction("Login");
            }
            PopulateViewBag();
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public IActionResult DisplayProfilePic(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null && !string.IsNullOrEmpty(user.ProfilePic))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, user.ProfilePic);
                if (System.IO.File.Exists(imagePath))
                {
                    var imageFileStream = System.IO.File.OpenRead(imagePath);
                    return File(imageFileStream, user.ContentType);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult RemoveProfilePic(int userId)
        {
            var user = _context.Users.Find(userId);

            if (user != null && user.ProfilePic != "ProfilePics/default_user.png")
            {
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, user.ProfilePic);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                user.ProfilePic = "ProfilePics/default_user.png";
                user.ContentType = "image/png";
                _context.Users.Update(user);
                _context.SaveChanges();
            }

            return RedirectToAction("EditProfile");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                ModelState.AddModelError("", "Username not found.");
                return View();
            }
            TempData["Username"] = username;
            return RedirectToAction("ResetPassword");
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            ViewBag.IsResetPasswordPage = true;
            if (TempData["Username"] == null)
            {
                return RedirectToAction("ForgotPassword");
            }
            ViewBag.Username = TempData["Username"];
            return View(new User { Username = TempData["Username"].ToString() });
        }

        [HttpPost]
        public IActionResult ResetPassword(User user)
        {
            ModelState.Remove("Username");
            ModelState.Remove("Name");
            ModelState.Remove("Age");
            ModelState.Remove("DateOfBirth");
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
                if (existingUser == null)
                {
                    ModelState.AddModelError("", "User not found.");
                    return View(user);
                }

                existingUser.Password = user.Password;
                existingUser.ConfirmPassword = user.ConfirmPassword;
                _context.Users.Update(existingUser);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Password reset successfully. Please log in with your new password.";
                return RedirectToAction("Login");
            }

            return View(user);
        }
    }
}
