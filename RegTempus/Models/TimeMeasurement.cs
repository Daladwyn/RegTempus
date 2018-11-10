using System;
using System.ComponentModel.DataAnnotations;

namespace RegTempus.Models
{
    public class TimeMeasurement
    {
        public int TimeMeasurementId { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeStop { get; set; }

        public DateTime TimeRegistered { get; set; }

        public string TimeType { get; set; }

        [Range(1,31)]
        public int DayOfMonth { get; set; }

        [Range(1,12)]
        public int MonthOfYear { get; set; }

    }
}
