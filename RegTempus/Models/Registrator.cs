using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RegTempus.Interfaces;

namespace RegTempus.Models
{
    public class Registrator
    {
        private IRegTempus _iRegTempus;

        public Registrator()
        {

        }

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

        public bool DoesRegistratorDataExits(Registrator registrator)
        {
            Registrator result = _iRegTempus.GetRegistratorBasedOnUserId(registrator);
            return ((result == null) ? false : true);
        }
    }

}
