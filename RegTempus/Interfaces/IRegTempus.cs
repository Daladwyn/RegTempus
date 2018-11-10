using RegTempus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegTempus.Interfaces
{
    public interface IRegTempus
    {
        void CreateNewMeasurement(DateTime TimeStart);
        void CompleteTimeMeasurement(DateTime TimeStop, int TimeMeasurementId);
    }
}
