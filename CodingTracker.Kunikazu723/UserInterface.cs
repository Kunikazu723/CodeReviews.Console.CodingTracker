using Spectre.Console;
using System.Reflection;

namespace CodingTracker.Kunikazu723
{
    public class UserInterface
    {
        private readonly UserInput _userInput;
        public UserInterface(UserInput userInput)
        {
            _userInput = userInput;
        }
        public void MainMenu()
        {
            WriteBanner();
            // Main Loop


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
