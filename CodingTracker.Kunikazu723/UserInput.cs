using CodingTracker.Kunikazu723.Enums;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CodingTracker.Kunikazu723.Enums.MenuEnums;

namespace CodingTracker.Kunikazu723
{
    internal class UserInput
    {
        public MainMenuOptions GetUserMainMenuOption()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                .Title("What do you want to do?")
                .AddChoices(Enum.GetValues<MainMenuOptions>())
                .UseConverter(OptionToString));
        }
    }
}
