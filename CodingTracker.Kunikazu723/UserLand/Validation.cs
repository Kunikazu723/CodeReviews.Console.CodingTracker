using System.Globalization;

namespace CodingTracker.Kunikazu723.UserLand
{
    public class Validation
    {
        public readonly string DateFormat;
        public Validation(string dateFormat)
        {
            DateFormat = dateFormat;
        }
        public bool IsDateTimeValidFormat(string date, out DateTime parsedDate) => DateTime.TryParseExact(date, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
        public bool AreDatesChronological(string startTime, string endTime)
        {
            DateTime parsedStartTime;
            DateTime parsedEndTime;
            if (!IsDateTimeValidFormat(startTime, out parsedStartTime) || !IsDateTimeValidFormat(endTime, out parsedEndTime))
            {
                return false;
            }

            return parsedEndTime > parsedStartTime;
        }
    }
}
