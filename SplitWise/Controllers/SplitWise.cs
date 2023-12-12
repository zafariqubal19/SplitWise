using Microsoft.AspNetCore.Mvc;
using SplitWise.Facade;
using SplitWise.Models;

namespace SplitWise.Controllers
{
    public class SplitWise : Controller
    {
        private IApplicatioFacade _applicatioFacade;
        public SplitWise(IApplicatioFacade applicatioFacade)
        {
            _applicatioFacade = applicatioFacade;
        }
        public IActionResult GetAllUsers()
        {
            List<User> users = _applicatioFacade.GetAllUser().Result;
            return View(users);
        }
        public IActionResult Split() { 

        return View();
        }
        public IActionResult CreateGroup()
        {
            return View();
        }
  
        [HttpGet]
        public IActionResult RegisterUser() 
        { 
            return View(); 
        }
        [HttpPost]
        public IActionResult RegisterUser(User user) 
        {
            return View();
        }
    }
}
