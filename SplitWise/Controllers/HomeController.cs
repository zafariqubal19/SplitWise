using Microsoft.AspNetCore.Mvc;
using SplitWise.Facade;
using SplitWise.Models;
using System.Diagnostics;

namespace SplitWise.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApplicatioFacade _applicationFacade;

        public HomeController(ILogger<HomeController> logger,IApplicatioFacade applicatioFacade)
        {
            _applicationFacade = applicatioFacade;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login login)
        {
            if(login.Email== "iquba7992@gmail.com" && login.Password=="zafar") {
               // TempData["Navbar"] = "True";
            return RedirectToAction("Index", "Home");   
            }
            else
            {
                TempData["Navbar"] = "false";

                return View();
            }
           
        }
        [HttpGet]
        public IActionResult Register()
        {
        return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            User users=_applicationFacade.RegisterUser(user).Result;
            if(users!=null)
            {
                TempData["UserRegister"] = "True";
                  
            }
            else
            {
                TempData["UserRegister"] = "False";
            }
           
              return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}