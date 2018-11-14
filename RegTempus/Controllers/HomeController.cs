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

        public IActionResult Index()
        {
            ClaimsPrincipal user = User;
            Registrator registrator = Registrator.GetRegistratorData(user);
            bool result = Registrator.DoesRegistratorDataExitsInDatabase(registrator);
            if (result==false)
            {
                registrator=Registrator.CreateNewRegistratorToStore(registrator);
            }
          //konvertera Registrator objekt till UserTimeRegistrationViewModel
            return View(registrator);
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