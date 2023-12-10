using Microsoft.AspNetCore.Mvc;

namespace SplitWise.Controllers
{
    public class SplitWise : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Split() { 

        return View();
        }
        public IActionResult CreateGroup()
        {
            return View();
        }
    }
}
