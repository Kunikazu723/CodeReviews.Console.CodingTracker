using CodingTracker.Kunikazu723.Dao.Interfaces;
using CodingTracker.Kunikazu723.Models;
using CodingTracker.Kunikazu723.Services;
using Microsoft.VisualBasic;
using Spectre.Console;
using System.Data.SQLite;
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
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                WriteBanner(Color.Blue);
                MainMenuOptions option = _userInput.PromptUserEnumOption<MainMenuOptions>();
                Console.Clear();
                try
                {
                    ExecuteOption(option);
                } catch (SQLiteException ex)
                {
                    // TODO: Make specific messages for error code (Like 'invalid ID' for a specific error code)
                    DisplayMessage($"DATABASE ERROR: \n\t{ex.Message}", Color.Red);
                }
                if (option == MainMenuOptions.Exit)
                    isRunning = false;

                AnsiConsole.MarkupLine("[bold grey]Press Any Key To Continue[/]");
                Console.ReadKey();
            }
            
        }

        private void ExecuteOption(MainMenuOptions option)
        {
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
                    break;
                default:
                    break;
            }
        }

        private int PromptForSessionId()
        {
            WriteBanner(Color.Red, "Delete Session");
            ViewSessions();
            return _userInput.AskUserForPositiveInteger("Enter the Session's ID");
        }

        private void DeleteSession()
        {
            int id = PromptForSessionId(Color.Red, "Delete Session");
            _dao.DeleteItemById(id);
        }

        private void UpdateSession()
        {
            int id = PromptForSessionId(Color.Cyan, "Update Session");
            _dao.UpdateItemById(id);
        }

        private void LogSession()
        {
            WriteBanner(Color.Yellow, "Log Session");
            var codingSession = _userInput.GetUserCodingSession();

            _dao.InsertItem(codingSession);
        }

        private void ViewSessions()
        {
            var sessionsTable = _service.GenerateCodingSessionsTable(_dao.GetAllItems(), "Coding Sessions");
            AnsiConsole.Write(sessionsTable);
        }

        public void WriteBanner(Color color, string bannerText = "CODING TRACKER")
        {
            var figletFont = FigletFont.Load("./FigletFonts/smkeyboard.flf");
            var appFiglet = new FigletText(figletFont, bannerText)
                .Color(color);

            var ruleStyle = Style.Parse(color.ToMarkup());
            AnsiConsole.Write(new Rule().RuleStyle(ruleStyle));
            AnsiConsole.Write(appFiglet);
            AnsiConsole.Write(new Rule().RuleStyle(ruleStyle));
        }

        void DisplayMessage(string message, Color color)
        {
            AnsiConsole.MarkupLine($"[{color.ToMarkup()}]{Markup.Escape(message)}[/]");
        }
    }
}
