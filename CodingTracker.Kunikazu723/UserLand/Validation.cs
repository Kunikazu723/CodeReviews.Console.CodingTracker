using System.Globalization;

namespace CodingTracker.Kunikazu723.UserLand
{
    public class Validation
    {
        public bool IsDateTimeValid(string date, string format) => DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        
    }
}
