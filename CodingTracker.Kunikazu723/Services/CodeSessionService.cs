using CodingTracker.Kunikazu723.Dao.Interfaces;
using CodingTracker.Kunikazu723.Models;
using CodingTracker.Kunikazu723.UserLand;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Kunikazu723.Services
{
    internal class CodeSessionService : IService
    {
        private readonly IDao<CodingSession> _sessionDao;
        private readonly UserInput _userInput;
        private readonly string _dateFormat;
        public CodeSessionService(IDao<CodingSession> sessionDao, UserInput userInput, string dateFormat)
        {
            _sessionDao = sessionDao;
            _userInput = userInput;
            _dateFormat = dateFormat;
        }
        public void AddItem()
        {
            (string startTime, string endTime) = _userInput.GetStartEndDate();

            var codingSession = new CodingSession()
            {
                StartTime = startTime,
                EndTime = endTime
            };

            _sessionDao.InsertItem(codingSession);
        }

        public void DeleteItem()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem()
        {
            throw new NotImplementedException();
        }

        public void ViewAllItems()
        {
            List<CodingSession> codingSessions = _sessionDao.GetAllItems();
            // TODO: Instead of displaying it here in the service. Make the service create a table that will be presented at the UserInterface.
            foreach (CodingSession codingSession in codingSessions)
            {
                try
                {
                    DateTime parsedStartTime = ParseStringTime(codingSession.StartTime);
                    DateTime parsedEndTime = ParseStringTime(codingSession.EndTime);
                    TimeSpan duration = (parsedEndTime - parsedStartTime);
                    Console.WriteLine($"ID: {codingSession.Id} Start: {codingSession.StartTime} End: {codingSession.EndTime} Duration: {duration.Hours} hours {duration.Minutes} minutes and {duration.Seconds} seconds");

                } catch (ArgumentException ex)
                {
                    AnsiConsole.MarkupLine($"[bold red]WARNING: \t{ex.Message}");
                }
                
            }
        }

        private DateTime ParseStringTime(string time)
        {
            DateTime result;
            if (!DateTime.TryParseExact(time, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                throw new ArgumentException($"Time String {time} does not follow proper date format {_dateFormat}");
            }
            return result;
        }
    }
}
