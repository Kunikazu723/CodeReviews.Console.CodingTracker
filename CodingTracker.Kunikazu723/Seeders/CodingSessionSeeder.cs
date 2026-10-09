using CodingTracker.Kunikazu723.Dao;
using CodingTracker.Kunikazu723.Dao.Interfaces;
using CodingTracker.Kunikazu723.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static CodingTracker.Kunikazu723.Enums.DateEnums;

namespace CodingTracker.Kunikazu723.Seeders
{
    internal class CodingSessionSeeder
    {
        private readonly IDao<CodingSession> _dao;
        private readonly string _dateFormat;
        private readonly Random _rng = new Random();
        private readonly List<DateTime> _unavailableDates = new List<DateTime>();
        public CodingSessionSeeder(IDao<CodingSession> dao, string dateFormat)
        {
            _dao = dao;
            _dateFormat = dateFormat;
        }

        public void SeedIfEmpty()
        {
            if (_dao.GetAllItems().Count == 0)
            {
                _dao.InsertMany(GenerateData());
            }
        }

        private List<CodingSession> GenerateData()
        {
            var data = new List<CodingSession>();
            DateTime sessionDate = new DateTime(2025, 01, 01);
            for (int i = 0; i < 100; i++)
            {
                DateTime startTime = GenerateDate(sessionDate, 12, TimeType.Hour);
                DateTime endTime = GenerateDate(startTime, 5, TimeType.Hour);

                data.Add(
                    new CodingSession() 
                    {
                        StartTime = startTime.ToString(_dateFormat),
                        EndTime = endTime.ToString(_dateFormat),
                    });

                sessionDate = GenerateDate(endTime, 7, TimeType.Day);
            }

            return data;
        }

        private DateTime GenerateDate(DateTime startDate, int range, TimeType rangeType)
        {
            if (range <= 0)
            {
                throw new ArgumentException("range cannot be lesser than 1");
            }
            int randomNumber = _rng.Next(1, range);
            return rangeType switch 
            { 
                TimeType.Day => startDate.AddDays(randomNumber),
                TimeType.Hour => startDate.AddHours(randomNumber),
                TimeType.Minute => startDate.AddMinutes(randomNumber),
                TimeType.Second => startDate.AddSeconds(randomNumber),
                _ => throw new NotImplementedException()
            };
        }
    }
}
