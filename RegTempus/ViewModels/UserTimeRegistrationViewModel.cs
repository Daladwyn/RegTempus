using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegTempus.ViewModels
{
    public class UserTimeRegistrationViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool UserHaveStartedTimeMeasure { get; set; }
    }
}
