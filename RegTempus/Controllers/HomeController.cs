using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegTempus.Models;
using RegTempus.Services;
using RegTempus.ViewModels;

namespace RegTempus.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IRegTempus _iRegTempus;

        //public Registrator() { }

        public HomeController(IRegTempus iRegTempus)
        {
            _iRegTempus = iRegTempus;
        }
        public IActionResult Index()
        {
            ClaimsPrincipal user = new ClaimsPrincipal();
            try
            {
                user = User;
            }
            catch (ArgumentNullException)
            {
                ViewBag.ErrorMessage = "No logged in user could be found.";
                return View();
            }
            Registrator registrator = Registrator.GetRegistratorData(user);
            registrator = _iRegTempus.GetRegistratorBasedOnUserId(registrator);
            bool result = ((registrator == null) ? false : true);
            if (result == false)
            {
                registrator = Registrator.GetRegistratorData(user);
                registrator.UserHaveStartedTimeMeasure = false;
                registrator.StartedTimeMeasurement = 0;
                registrator = _iRegTempus.CreateRegistrator(registrator);
            }
            UserTimeRegistrationViewModel konvertedRegistrator = UserTimeRegistrationViewModel.RestructureTheRegistratorData(registrator);
            return View(konvertedRegistrator);
        }



        [HttpPost]
        public IActionResult StartTime(int userId)
        {
            //leta upp registrator mha userId och sätt att en registration är påbörjad.
            //Skapa en ny tidsregistrering objekt och sätt start, stop och summa tiden till aktuell tid.
            //skapa en viewbag med ett framgångsmeddelande.
            return View();
        }

        [HttpPost]
        public IActionResult StopTime(int userId)
        {
            return View();
        }
    }
}