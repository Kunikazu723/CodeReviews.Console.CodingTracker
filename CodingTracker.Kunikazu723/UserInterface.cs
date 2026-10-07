using CodingTracker.Kunikazu723.Services;
using Spectre.Console;
using System.Reflection;
using static CodingTracker.Kunikazu723.Enums.MenuEnums;

namespace CodingTracker.Kunikazu723
{
    public class UserInterface
    {
        private readonly UserInput _userInput;
        private readonly IService _service;
        public UserInterface(UserInput userInput, IService service)
        {
            _userInput = userInput;
            _service = service;
        }
        public void MainMenu()
        {
            WriteBanner();
            // Main Loop
            bool isRunning = true;
            while (isRunning)
            {
                MainMenuOptions option = _userInput.PromptUserEnumOption<MainMenuOptions>();
                switch (option)
                {
                    case MainMenuOptions.ViewSessions:
                        _service.ViewAllItems();
                        break;
                    case MainMenuOptions.LogSessions:
                        _service.AddItem();
                        break;
                    case MainMenuOptions.UpdateSession:
                        _service.UpdateItem();
                        break;
                    case MainMenuOptions.DeleteSession:
                        _service.DeleteItem();
                        break;
                    case MainMenuOptions.Exit:
                        AnsiConsole.MarkupLine("Exiting [bold cyan]Coding Tracker[/]...");
                        isRunning = false;
                        break;
                    default:
                        break;
                }

                Console.ReadKey();
            }
            
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

        public void PromptUserMenuOption()
        {

        }
    }
}
