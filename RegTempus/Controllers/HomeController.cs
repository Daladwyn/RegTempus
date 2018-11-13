using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegTempus.ViewModels;

namespace RegTempus.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            var UserThatRegisterTime = new UserTimeRegistrationViewModel();
            UserThatRegisterTime.RegistratorId = 1;
            UserThatRegisterTime.FirstName = "Christian";
            UserThatRegisterTime.LastName = "Levin";
            UserThatRegisterTime.UserHaveStartedTimeMeasure = false;
            return View(UserThatRegisterTime);
        }

        [HttpPost]
        public IActionResult StartTime(int userId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult StopTime(int userId)
        {
            return View();
        }
    }
}