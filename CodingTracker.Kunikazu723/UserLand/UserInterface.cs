using CodingTracker.Kunikazu723.Dao.Interfaces;
using CodingTracker.Kunikazu723.Models;
using CodingTracker.Kunikazu723.Services;
using Microsoft.VisualBasic;
using Spectre.Console;
using System.Globalization;
using System.Reflection;
using static CodingTracker.Kunikazu723.Enums.MenuEnums;

namespace CodingTracker.Kunikazu723.UserLand
{
    public class UserInterface
    {
        private readonly UserInput _userInput;
        private readonly CodeSessionService _service;
        private readonly IDao<CodingSession> _dao;

        public UserInterface(UserInput userInput, CodeSessionService service, IDao<CodingSession> dao)
        {
            _userInput = userInput;
            _service = service;
            _dao = dao;
        }
        public void MainMenu()
        {
            WriteBanner();
            // Main Loop
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                MainMenuOptions option = _userInput.PromptUserEnumOption<MainMenuOptions>();
                switch (option)
                {
                    case MainMenuOptions.ViewSessions:
                        ViewSessions();
                        break;
                    case MainMenuOptions.LogSessions:
                        LogSession();
                        break;
                    case MainMenuOptions.UpdateSession:
                        UpdateSession();
                        break;
                    case MainMenuOptions.DeleteSession:
                        DeleteSession();
                        break;
                    case MainMenuOptions.Exit:
                        AnsiConsole.MarkupLine("Exiting [bold cyan]Coding Tracker[/]...");
                        isRunning = false;
                        break;
                    default:
                        break;
                }
                AnsiConsole.MarkupLine("[bold grey]Press Any Key To Continue[/]");
                Console.ReadKey();
            }
            
        }

        private void DeleteSession()
        {
            throw new NotImplementedException();
        }

        private void UpdateSession()
        {
            throw new NotImplementedException();
        }

        private void LogSession()
        {
            (string startTime, string endTime) = _userInput.GetStartEndDate();

            var codingSession = new CodingSession()
            {
                StartTime = startTime,
                EndTime = endTime
            };

            _dao.InsertItem(codingSession);
        }

        private void ViewSessions()
        {
            var sessionsTable = _service.GenerateCodingSessionsTable(_dao.GetAllItems());
            AnsiConsole.Write(sessionsTable);
        }

        public void WriteBanner(string bannerText = "CODING TRACKER")
        {
            var figletFont = FigletFont.Load("./FigletFonts/smkeyboard.flf");
            var appFiglet = new FigletText(figletFont, "CODING TRACKER")
                .Color(Color.Blue);

            AnsiConsole.Write(new Rule().RuleStyle(Style.Parse("blue dim")));
            AnsiConsole.Write(appFiglet);
            AnsiConsole.Write(new Rule().RuleStyle(Style.Parse("blue dim")));
        }
    }
}
