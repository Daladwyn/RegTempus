using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegTempus.ViewModels;

namespace RegTempus.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var UserThatRegisterTime = new UserTimeRegistrationViewModel();
            UserThatRegisterTime.FirstName = "Christian";
            UserThatRegisterTime.LastName = "Levin";
            UserThatRegisterTime.UserHaveStartedTimeMeasure = false;
            return View(UserThatRegisterTime);
        }

    }
}