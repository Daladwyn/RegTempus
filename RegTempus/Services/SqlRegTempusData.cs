﻿using System;
using System.Collections.Generic;
using System.Linq;
using RegTempus.Interfaces;
using RegTempus.Models;

namespace RegTempus.Services
{
    public class SqlRegTempusData : IRegTempus
    {
        private RegTempusDbContext _context;

        public SqlRegTempusData(RegTempusDbContext context)
        {
            _context = context;
        }

        public TimeMeasurement CompleteTimeMeasurement(TimeMeasurement timeStop)
        {
            _context.TimeMeasurements.Attach(timeStop);
            _context.SaveChanges();
            return timeStop;
        }

        public TimeMeasurement CreateNewMeasurement(TimeMeasurement timeStart)
        {
            _context.TimeMeasurements.Add(timeStart);
            _context.SaveChanges();
            return timeStart;
        }

        public Registrator CreateRegistrator(Registrator user)
        {
            _context.Registrators.Add(user);
            _context.SaveChanges();
            return user;
        }

        public IEnumerable<TimeMeasurement> GetMonthlyTimeMeasurement(int monthOfYear)
        {
            return _context.TimeMeasurements.Where(m => m.MonthOfYear == monthOfYear).ToList();
        }

        public Registrator GetRegistrator(Registrator user)
        {
            return _context.Registrators.SingleOrDefault(r => r.RegistratorId == user.RegistratorId);
        }

        public Registrator UpdateRegistrator(Registrator user)
        {
            _context.Registrators.Attach(user);
            _context.SaveChanges();
            return user;
        }
    }
}