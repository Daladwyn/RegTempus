using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegTempus.ViewModels
{
    public class PresentRegisteredTimeViewModel
    {

        [MaxLength(10)]
        public string Month { get; set; }

        [Range(1,31)]
        public int Day { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeStop { get; set; }

        public DateTime TimeBreak { get; set; }
    }
}
