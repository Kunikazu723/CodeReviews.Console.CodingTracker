using Spectre.Console;
using static CodingTracker.Kunikazu723.Enums.MenuEnums;

namespace CodingTracker.Kunikazu723
{
    internal class UserInput
    {
        public T PromptUserEnumOption<T>() where T: struct, Enum
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<T>()
                .Title("What do you want to do?")
                .AddChoices(Enum.GetValues<T>())
                .UseConverter(OptionToString));
        }
    }
}
