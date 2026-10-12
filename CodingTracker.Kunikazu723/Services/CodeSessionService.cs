using CodingTracker.Kunikazu723.Dao.Interfaces;
using CodingTracker.Kunikazu723.Models;
using CodingTracker.Kunikazu723.UserLand;
using Spectre.Console;
using System.Globalization;

namespace CodingTracker.Kunikazu723.Services
{
    public class CodeSessionService 
    {
        private readonly IDao<CodingSession> _sessionDao;
        private readonly string _dateFormat;
        public CodeSessionService(IDao<CodingSession> sessionDao, string dateFormat)
        {
            _sessionDao = sessionDao;
            _dateFormat = dateFormat;
        }

        public Table GenerateCodingSessionsTable(List<CodingSession> codingSessions, string? tableTitle = null)
        {
            string durationFormat = @"hh\:mm\:ss";
            var table = new Table();
            if (tableTitle is not null) 
                table.Title(tableTitle);

            table.AddColumns("Id", "Start Time", "End Time", $"Duration ({durationFormat})");
            foreach (CodingSession codingSession in codingSessions)
            {
                try
                {
                    DateTime parsedStartTime = ParseStringTime(codingSession.StartTime);
                    DateTime parsedEndTime = ParseStringTime(codingSession.EndTime);
                    TimeSpan duration = (parsedEndTime - parsedStartTime);
                    table.AddRow(codingSession.Id.ToString(), codingSession.StartTime, codingSession.EndTime, duration.ToString(durationFormat));

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
