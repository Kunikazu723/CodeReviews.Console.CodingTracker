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
            MainMenuOptions option = _userInput.PromptUserEnumOption<MainMenuOptions>();

            Console.ReadKey();
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
