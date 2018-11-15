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
                ViewBag.Message = "No logged in user could be found.";
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
        public IActionResult StartTime(int registratorId)
        {
            Registrator registrator = new Registrator
            {
                RegistratorId = registratorId
            };
            registrator = _iRegTempus.GetRegistratorBasedOnRegistratorId(registrator);
            registrator.UserHaveStartedTimeMeasure = true;
            registrator.StartedTimeMeasurement = 0;
            TimeMeasurement timeRegistration = new TimeMeasurement
            {
                TimeMeasurementId = 0,
                RegistratorId = registrator.RegistratorId,
                TimeStart = DateTime.Now,
                TimeStop = DateTime.Now,
                TimeRegistered = DateTime.Now,
                DayOfMonth = DateTime.Today.Day,
                MonthOfYear = DateTime.Today.Month,
                Year = DateTime.Today.Year,
                TimeType = "Work"
            };
            timeRegistration = _iRegTempus.CreateNewMeasurement(timeRegistration);
            bool result = timeRegistration.TimeMeasurementId == 0 ? false : true;
            registrator.StartedTimeMeasurement = timeRegistration.TimeMeasurementId;
            registrator = _iRegTempus.UpdateRegistrator(registrator);
            ViewBag.Message = result == true ? "Your time is registered" : "Ops, something have occured and your time is not registered.";
            UserTimeRegistrationViewModel konvertedRegistrator = UserTimeRegistrationViewModel.RestructureTheRegistratorData(registrator);
            return View("Index", konvertedRegistrator);
        }

        [HttpPost]
        public IActionResult StopTime(int userId)
        {
            return View();
        }
    }
}