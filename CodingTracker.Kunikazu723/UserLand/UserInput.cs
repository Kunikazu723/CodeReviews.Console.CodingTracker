using CodingTracker.Kunikazu723.Models;
using Spectre.Console;
using static CodingTracker.Kunikazu723.Enums.MenuEnums;

namespace CodingTracker.Kunikazu723.UserLand
{
    public class UserInput
    {
        private readonly Validation _validation;
        public UserInput(Validation validation)
        {
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

        public string GetDateTime()
        {
            while (true)
            {
                string date = AnsiConsole.Ask<string>("Enter the date in the format [bold yellow]dd-MM-yyyy[/]");
                string time = AnsiConsole.Ask<string>("Enter the time in the format HH-mm-ss");
                string userDateTime = date + '-' + time;
                if (_validation.IsDateTimeValidFormat(userDateTime, out _))
                {
                    return userDateTime;
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold red]Invalid Input[/]");
                }
            }
        }

        public (string StartTime, string EndTime) GetStartEndDate()
        {
            while (true)
            {
                AnsiConsole.MarkupLine("[bold yellow]Start Time[/]");
                string startTime = GetDateTime();

                Console.WriteLine();

                AnsiConsole.MarkupLine("[bold yellow]End Time[/]");
                string endTime = GetDateTime();

                if (_validation.AreDatesChronological(startTime, endTime))
                {
                    return (startTime, endTime);
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold red]Invalid Input: End Time is earlier than Start Time[/]");
                }
            }
        }
    }
}
