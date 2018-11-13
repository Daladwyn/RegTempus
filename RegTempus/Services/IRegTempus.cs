﻿using RegTempus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegTempus.Interfaces
{
    public interface IRegTempus
    {
        TimeMeasurement CreateNewMeasurement(TimeMeasurement timeStart);
        TimeMeasurement CompleteTimeMeasurement(TimeMeasurement timeStop);
        IEnumerable<TimeMeasurement> GetMonthlyTimeMeasurement(int monthOfYear);
        Registrator GetRegistrator(Registrator user);
        Registrator UpdateRegistrator(Registrator user);
        Registrator CreateRegistrator(Registrator user);
    }
}
