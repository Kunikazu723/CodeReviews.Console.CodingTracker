using System.Globalization;

namespace CodingTracker.Kunikazu723.UserLand
{
    public class Validation
    {
        private readonly string _dateFormat;
        public Validation(string dateFormat)
        {
            _dateFormat = dateFormat;
        }
        public bool IsDateTimeValidFormat(string date, out DateTime parsedDate) => DateTime.TryParseExact(date, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
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
