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
    public class CodeSessionService 
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

        public Table GenerateCodingSessionsTable(List<CodingSession> codingSessions)
        {
            var table = new Table().Title("Coding Sessions");
            table.AddColumns("Id", "Start Time", "End Time", "Duration (hh:mm:ss)");
            foreach (CodingSession codingSession in codingSessions)
            {
                try
                {
                    DateTime parsedStartTime = ParseStringTime(codingSession.StartTime);
                    DateTime parsedEndTime = ParseStringTime(codingSession.EndTime);
                    TimeSpan duration = (parsedEndTime - parsedStartTime);
                    table.AddRow(codingSession.Id.ToString(), codingSession.StartTime, codingSession.EndTime, duration.ToString(@"hh\:mm\:ss"));

                }
                catch (ArgumentException ex)
                {
                    AnsiConsole.MarkupLine($"[bold red]WARNING: session with ID: {codingSession.Id} \t{ex.Message}[/]");
                }

            }
            return table;
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
