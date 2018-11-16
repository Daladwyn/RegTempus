using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RegTempus.ViewModels;

namespace RegTempus.Models
{
    public class TimeMeasurement
    {
        public int TimeMeasurementId { get; set; }

        [Required]
        public int RegistratorId { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeStop { get; set; }

        public TimeSpan TimeRegistered { get; set; }

        public string TimeType { get; set; }

        [Range(1, 31)]
        public int DayOfMonth { get; set; }

        [Range(1, 12)]
        public int MonthOfYear { get; set; }

        [Range(1900, 2100)]
        public int Year { get; set; }

        public static TimeMeasurement startClock(Registrator registrator)
        {
            TimeMeasurement measuredTime = new TimeMeasurement
            {
                TimeMeasurementId = 0,
                RegistratorId = registrator.RegistratorId,
                TimeStart = DateTime.Now,
                TimeStop = DateTime.Now,
                //TimeRegistered = 0,
                DayOfMonth = DateTime.Today.Day,
                MonthOfYear = DateTime.Today.Month,
                Year = DateTime.Today.Year,
                TimeType = "Work"
            };
            return measuredTime;
        }

        public static TimeMeasurement stopClock(TimeMeasurement measuredTime)
        {
            measuredTime.TimeStop = DateTime.Now;
            measuredTime.TimeRegistered = measuredTime.TimeStop - measuredTime.TimeStart;
            return measuredTime;
        }

       
    }
}
