using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using RegTempus.Services;

namespace RegTempus.Models
{
    public class Registrator
    {
        private IRegTempus _iRegTempus;

        public Registrator() { }

        public Registrator(IRegTempus iRegTempus)
        {
            _iRegTempus = iRegTempus;
        }

        public int RegistratorId { get; set; }

        [MaxLength(36)]
        public string UserId { get; set; }

        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }

        public bool UserHaveStartedTimeMeasure { get; set; }

        public int StartedTimeMeasurement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrator"></param>
        internal Registrator CreateNewRegistratorToStore(Registrator registrator)
        {
            registrator.UserHaveStartedTimeMeasure = false;
            registrator.StartedTimeMeasurement = 0;
            registrator = _iRegTempus.CreateRegistrator(registrator);
            return registrator;
        }
        /// <summary>
        /// This function asks the database if a User exits based on a ObjectIdentifier
        /// that is recived from Azure AD.
        /// </summary>
        /// <param name="registrator"></param>
        /// <returns></returns>
        internal bool DoesRegistratorDataExitsInDatabase(Registrator registrator)
        {
            Registrator result = _iRegTempus.GetRegistratorBasedOnUserId(registrator);
            return ((result == null) ? false : true);
        }

        /// <summary>
        /// This function extracts 3 properties(ObjectIdentifier, Givenname and surname)
        /// of the User token from Azure AD.
        /// </summary>
        /// <returns>Output from this function is a Registrator object</returns>
        internal static Registrator GetRegistratorData(ClaimsPrincipal User)
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
                        if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")
                        {
                            registrator.LastName = claim.Value;
                        }
                    }
                }
            }
            return registrator;
        }
    }
}