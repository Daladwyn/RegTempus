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
            catch (NullReferenceException)
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
            try
            {
                registrator = _iRegTempus.GetRegistratorBasedOnRegistratorId(registrator);
            }
            catch (NullReferenceException)
            {
                ViewBag.Message = "Error: Did not succed in fetching userdata";
                return View("Index");
            }
            registrator.UserHaveStartedTimeMeasure = true;
            registrator.StartedTimeMeasurement = 0;
            TimeMeasurement timeRegistration = TimeMeasurement.startClock(registrator);
            try
            {
                timeRegistration = _iRegTempus.CreateNewMeasurement(timeRegistration);
            }
            catch (NullReferenceException)
            {
                ViewBag.Message = "Error: Saving the new time registration did not succed";
                return View("Index");
            }
            bool result = timeRegistration.TimeMeasurementId == 0 ? false : true;
            registrator.StartedTimeMeasurement = timeRegistration.TimeMeasurementId;
            try
            {
                registrator = _iRegTempus.UpdateRegistrator(registrator);
            }
            catch (NullReferenceException)
            {
                ViewBag.Message = "Error: Updating your data did not succed";
                return View("Index");
            }
            if (registrator.StartedTimeMeasurement != timeRegistration.TimeMeasurementId)
            {
                ViewBag.Message = "Error: changing your details did not succed";
            }
            ViewBag.Message = result == true ? "Your time is registered" : "Ops, something have occured and your time is not registered.";
            UserTimeRegistrationViewModel konvertedRegistrator = UserTimeRegistrationViewModel.RestructureTheRegistratorData(registrator);
            return View("Index", konvertedRegistrator);
        }

        [HttpPost]
        public IActionResult StopTime(int registratorId)
        {
            Registrator registrator = new Registrator
            {
                RegistratorId = registratorId
            };
            TimeMeasurement measuredTime = new TimeMeasurement();
            try
            {
                registrator = _iRegTempus.GetRegistratorBasedOnRegistratorId(registrator);
            }
            catch (NullReferenceException)
            {
                ViewBag.Message = "Fetching your data did not succed. Please make a manual note of present time.";
                return View("Index");
            }
            registrator.UserHaveStartedTimeMeasure = false;
            try
            {
                measuredTime = _iRegTempus.GetTimeMeasurement(registrator);
            }
            catch (NullReferenceException)
            {
                ViewBag.Message = "Fetching your start time did not succed. Please make a manual note of present time.";
                return View("Index");
            }
            measuredTime = TimeMeasurement.stopClock(measuredTime);
            try
            {
                measuredTime = _iRegTempus.CompleteTimeMeasurement(measuredTime);
            }
            catch (NullReferenceException)
            {
                ViewBag.Message = "Updating your start time did not succed. Please make a manual note of present time.";
                return View("Index");
            }
            registrator.StartedTimeMeasurement = 0;
            try
            {
                registrator = _iRegTempus.UpdateRegistrator(registrator);
            }
            catch (Exception)
            {
                ViewBag.Message = "Updating your data did not succed. Please make a manual note of present time.";
                return View("Index");
            }
            UserTimeRegistrationViewModel konvertedRegistrator = UserTimeRegistrationViewModel.RestructureTheRegistratorData(registrator);
            return View("Index", konvertedRegistrator);
        }


    }
}