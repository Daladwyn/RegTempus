using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegTempus.Models;
using RegTempus.ViewModels;

namespace RegTempus.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //private Registrator _registrator;

        //public HomeController(Registrator registrator)
        //{
        //    _registrator = registrator;
        //}
        public IActionResult Index()
        {
            Registrator registrator = new Registrator();

            if (User.Identity.IsAuthenticated)
            {
                foreach (var Identity in User.Identities)
                {
                    foreach (var claim in Identity.Claims)
                    {
                        if (claim.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")
                        {
                            registrator.UserId = claim.Value;
                        }
                        if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")
                        {
                            registrator.FirstName = claim.Value;
                        }
                        if (claim.Type== "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")
                        {
                            registrator.LastName = claim.Value;
                        }
                    }
                }
                
            
            }

            bool result = Registrator.DoesRegistratorDataExits(registrator);
            //var UserThatRegisterTime = new UserTimeRegistrationViewModel();
            //UserThatRegisterTime.RegistratorId = 1;
            //UserThatRegisterTime.FirstName = "Christian";
            //UserThatRegisterTime.LastName = "Levin";
            //UserThatRegisterTime.UserHaveStartedTimeMeasure = false;
            //return View(UserThatRegisterTime);
            return View();
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