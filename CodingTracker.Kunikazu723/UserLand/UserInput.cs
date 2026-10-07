using CodingTracker.Kunikazu723.Models;
using Spectre.Console;
using static CodingTracker.Kunikazu723.Enums.MenuEnums;

namespace CodingTracker.Kunikazu723.UserLand
{
    public class UserInput
    {
        private readonly string _dateTimeFormat;
        private readonly Validation _validation;
        public UserInput(string dateTimeFormat, Validation validation)
        {
            _dateTimeFormat = dateTimeFormat;
            _validation = validation;
        }
        public T PromptUserEnumOption<T>() where T: struct, Enum
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<T>()
                .Title("What do you want to do?")
                .AddChoices(Enum.GetValues<T>())
                .UseConverter(OptionToString));
        }
        public CodingSession GetUserCodingSession()
        {
            return new CodingSession();
        }

        public string? GetDateTime()
        {
            string date = AnsiConsole.Ask<string>("Enter the date in the format [bold yellow]dd-MM-yyyy[/]");
            string time = AnsiConsole.Ask<string>("Enter the time in the format HH-mm-ss");
            string userDateTime = date + '-' + time;
            if (_validation.IsDateTimeValid(userDateTime, _dateTimeFormat))
            {
                return userDateTime;
            }
            return null;
        }
    }
}
