using Healthcare_And_Wellness.Models;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_And_Wellness.Controllers
{
    public class BmiCalculatorController : Controller
    {
        private void PopulateViewBag()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
        }

        [HttpGet]
        public IActionResult BmiCalculation()
        {
            PopulateViewBag();
            return View(new BmiCalculator());
        }

        [HttpPost]
        public IActionResult BmiCalculation(BmiCalculator bmiCalculator)
        {
            PopulateViewBag();
            if (ModelState.IsValid)
            {
                bmiCalculator.Result = bmiCalculator.Calculate();
                return View(bmiCalculator);
            }
            return View(new BmiCalculator());
        }
    }
}
