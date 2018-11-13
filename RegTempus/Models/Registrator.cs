using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegTempus.Models
{
    public class Registrator
    {
        public int RegistratorId { get; set; }

        [MaxLength(36)]
        public string UserId { get; set; }

        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }

        public bool UserHaveStartedTimeMeasure { get; set; }

        public int StartedTimeMeasurement { get; set; }

    }
}
