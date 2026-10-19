using System.Text.RegularExpressions;

namespace CodingTracker.Kunikazu723.Enums
{
    internal static class MenuEnums
    {
        public enum MainMenuOptions
        {
            ViewSessions,
            LogSessions,
            UpdateSession,
            DeleteSession,
            Exit
        }

        public static string OptionToString<T>(T option) where T: struct, Enum => Regex.Replace(option.ToString(), "([A-Z])", " $1").Trim();
        
    }
}
