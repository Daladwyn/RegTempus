using RegTempus.Interfaces;
using RegTempus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegTempus.Repositories
{
    public class RegTempusRepository : IRegTempus
    {
        private readonly AppDbContext _appDbContext;

        public RegTempusRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void CompleteTimeMeasurement(DateTime TimeStop, int TimeMeasurementId)
        {
            throw new NotImplementedException();
        }

        public void CreateNewMeasurement(DateTime TimeStart)
        {
            //var measurement = new TimeMeasurement();
            throw new NotImplementedException();
        }
    }
}
