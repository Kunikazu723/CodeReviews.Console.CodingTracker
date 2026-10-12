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
        private readonly TableEngine _tableEngine;
        private readonly IDao<CodingSession> _dao;

        public UserInterface(UserInput userInput, TableEngine engine, IDao<CodingSession> dao)
        {
            _userInput = userInput;
            _tableEngine = engine;
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
                    DisplayMessage("Invalid Option", Color.Red);
                    break;
            }
        }

        private int PromptForSessionId()
        {
            ViewSessions();
            return _userInput.AskUserForPositiveInteger("Enter the Session's ID");
        }

        private CodingSession? PromptForExistingSession()
        {
            int id = PromptForSessionId();
            var codingSession = _dao.GetItemById(id);
            if (codingSession is null)
            {
                DisplayMessage($"Coding Session With ID {id} not found\nAborting process", Color.Red);
            }
            return codingSession;
        }

        private void DeleteSession()
        {
            WriteBanner(Color.Red, "Delete Session");
            var codingSession = PromptForExistingSession();
            if (codingSession is null) return;

            Table sessionTable = _tableEngine.GenerateCodingSessionsTable(new List<CodingSession> {codingSession}, "To be Deleted");
            AnsiConsole.Write(sessionTable);

            bool confirmDeletion = AnsiConsole.Confirm("Are you sure you wish to delete permanently this session?", false);
            if (confirmDeletion)
            {
                _dao.DeleteItemById(codingSession);
                DisplayMessage("Session Deleted successfully", Color.Green);
            }
            else
            {
                DisplayMessage("Deletion aborted", Color.Green);
            }
        }

        private void UpdateSession()
        {
            WriteBanner(Color.Cyan, "Update Session");
            var currentCodingSession = PromptForExistingSession();

            if (currentCodingSession is null) return;

            Table currentItemTable = _tableEngine.GenerateCodingSessionsTable(new List<CodingSession> { currentCodingSession }, "Current Coding Session");
            AnsiConsole.Write(currentItemTable);

            AnsiConsole.MarkupLine("[bold yellow]Enter new Coding Sessino Data[/]");
            var newCodingSession = _userInput.GetUserCodingSession();
            newCodingSession.Id = currentCodingSession.Id;

            Table newItemTable = _tableEngine.GenerateCodingSessionsTable(new List<CodingSession> { newCodingSession }, "Updated Coding Session");
            AnsiConsole.Write(newItemTable);

            bool confirmUpdate = AnsiConsole.Confirm("Do you wish to apply the Update?", false);

            if (confirmUpdate)
            {
                _dao.UpdateItemById(newCodingSession);
                DisplayMessage("Coding session updated successfuly", Color.Green);
            }
            else
            {
                DisplayMessage("Update Aborted. No changes made.", Color.Green);
            }
        }

        private void LogSession()
        {
            WriteBanner(Color.Yellow, "Log Session");
            var codingSession = _userInput.GetUserCodingSession();

            _dao.InsertItem(codingSession);
            DisplayMessage("New coding session Successfuly Logged", Color.Green);
        }

        private void ViewSessions()
        {
            var sessionsTable = _tableEngine.GenerateCodingSessionsTable(_dao.GetAllItems(), "Coding Sessions");
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
