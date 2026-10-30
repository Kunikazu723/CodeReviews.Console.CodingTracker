using CodingTracker.Kunikazu723.Dao.Interfaces;
using CodingTracker.Kunikazu723.Models;
using CodingTracker.Kunikazu723.UserLand;
using Spectre.Console;
using System.Globalization;

namespace CodingTracker.Kunikazu723.Services
{
    public class TableEngine 
    {
        private readonly string _dateFormat;
        public TableEngine(string dateFormat)
        {
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
                    TimeSpan duration = (codingSession.EndTime - codingSession.StartTime);
                    table.AddRow(codingSession.Id.ToString(), codingSession.StartTime.ToString(_dateFormat), codingSession.EndTime.ToString(_dateFormat), duration.ToString(durationFormat));

                }
                catch (ArgumentException ex)
                {
                    AnsiConsole.MarkupLine($"[bold red]WARNING: session with ID: {codingSession.Id} \t{ex.Message}[/]");
                }

            }
            return table;
        }
    }
}
