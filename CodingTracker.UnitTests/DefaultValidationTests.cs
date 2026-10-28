using CodingTracker.Kunikazu723.UserLand;

namespace CodingTracker.UnitTests
{
    public class DefaultValidationTests
    {
        private Validation _validation = null!;

        [SetUp]
        public void Setup()
        {
            _validation = new Validation("dd-MM-yyyy-HH-mm-ss");
        }

        private DateTime Parse(string input)
        {
            Assert.That(_validation.IsDateTimeValidFormat(input, out var date), Is.True, $"Test data '{input}' should be valid");
            return date;
        }

        [TestCase("22-10-2026-14-30-45", TestName = "Typical date")]
        [TestCase("01-01-2026-00-00-00", TestName = "Start of year, midnight")]
        [TestCase("31-12-2026-23-59-59", TestName = "End of year, last second")]
        [TestCase("29-02-2024-12-00-00", TestName = "Leap day, leap year")]
        [TestCase("28-02-2023-12-00-00", TestName = "Last Feb day, non-leap year")]
        [TestCase("30-04-2026-10-10-10", TestName = "30-day month, day 30")]
        public void IsDateTimeValidFormat_CorrectFormat_ReturnsTrue(string inputDate)
        {
            bool isValid = _validation.IsDateTimeValidFormat(inputDate, out _);
            Assert.That(isValid, Is.True);
        }

        // Out-of-range values
        [TestCase("29-02-2023-12-00-00", TestName = "Feb 29 in non-leap year")]
        [TestCase("31-04-2026-12-00-00", TestName = "Day 31 in 30-day month")]
        [TestCase("32-01-2026-12-00-00", TestName = "Day 32")]
        [TestCase("00-01-2026-12-00-00", TestName = "Day zero")]
        [TestCase("15-13-2026-12-00-00", TestName = "Month 13")]
        [TestCase("15-00-2026-12-00-00", TestName = "Month zero")]
        [TestCase("15-06-2026-24-00-00", TestName = "Hour 24")]
        [TestCase("15-06-2026-12-60-00", TestName = "Minute 60")]
        [TestCase("15-06-2026-12-00-60", TestName = "Second 60")]
        [TestCase("15-06-0000-12-00-00", TestName = "Year zero")]
        // Wrong shape
        [TestCase("22/10/2026/14/30/45", TestName = "Wrong separator")]
        [TestCase("22-10-2026 14:30:45", TestName = "Mixed separators")]
        [TestCase("22-10-2026-14-30", TestName = "Missing seconds")]
        [TestCase("22-10-2026-14-30-45-99", TestName = "Extra part")]
        [TestCase("2026-10-22-14-30-45", TestName = "Year first")]
        [TestCase("22-10-26-14-30-45", TestName = "Two-digit year")]
        [TestCase("2-10-2026-14-30-45", TestName = "Single-digit day")]
        [TestCase("22-10-2026-4-30-45", TestName = "Single-digit hour")]
        // Garbage and edge input
        [TestCase("ab-cd-efgh-ij-kl-mn", TestName = "Letters")]
        [TestCase("22-10-2026-14-30-4x", TestName = "One bad character")]
        [TestCase(" 22-10-2026-14-30-45", TestName = "Leading space")]
        [TestCase("22-10-2026-14-30-45 ", TestName = "Trailing space")]
        [TestCase("", TestName = "Empty string")]
        [TestCase("   ", TestName = "Whitespace only")]
        [TestCase(null, TestName = "Null")]
        public void IsDateTimeValidFormat_InvalidInput_ReturnsFalse(string? input)
        {
            bool isValid = _validation.IsDateTimeValidFormat(input!, out _);

            Assert.That(isValid, Is.False);
        }

        [TestCase("22-10-2026-14-00-00", "22-10-2026-15-30-00", TestName = "Same day, end later")]
        [TestCase("22-10-2026-23-00-00", "23-10-2026-01-00-00", TestName = "Crosses midnight")]
        [TestCase("31-12-2026-23-59-59", "01-01-2027-00-00-01", TestName = "Crosses year boundary")]
        public void AreDatesChronological_ValidInput_ReturnsTrue(string start, string end)
        {
            Assert.That(_validation.AreDatesChronological(Parse(start), Parse(end)), Is.True);
        }

        [TestCase("22-10-2026-15-30-00", "22-10-2026-14-00-00", TestName = "End before start")]
        [TestCase("23-10-2026-01-00-00", "22-10-2026-23-00-00", TestName = "End on previous day")]
        [TestCase("22-10-2026-14-00-00", "22-10-2026-14-00-00", TestName = "Equal times")]
        public void AreDatesChronological_EndNotAfterStart_ReturnsFalse(string start, string end)
        {
            Assert.That(_validation.AreDatesChronological(Parse(start), Parse(end)), Is.False);
        }
    }

    public class IsoValidationTests
    {
        private Validation _validation = null!;

        [SetUp]
        public void SetUp()
        {
            _validation = new Validation("yyyy-MM-dd'T'HH:mm:ss");
        }

        private DateTime Parse(string input)
        {
            Assert.That(_validation.IsDateTimeValidFormat(input, out var date), Is.True, $"Test data '{input}' should be valid");
            return date;
        }

        // Valid inputs
        [TestCase("2026-10-22T14:30:45", TestName = "Typical date")]
        [TestCase("2026-01-01T00:00:00", TestName = "Start of year")]
        [TestCase("2026-12-31T23:59:59", TestName = "End of year")]
        [TestCase("2024-02-29T12:00:00", TestName = "Leap day, leap year")]
        [TestCase("2023-02-28T12:00:00", TestName = "Last Feb day, non-leap year")]
        [TestCase("2026-04-30T10:10:10", TestName = "30-day month, day 30")]
        public void IsDateTimeValidFormat_ValidInput_ReturnsTrue(string input)
        {
            bool isValid = _validation.IsDateTimeValidFormat(input, out _);

            Assert.That(isValid, Is.True);
        }

        // Invalid inputs
        [TestCase("2023-02-29T12:00:00", TestName = "Feb 29 in non-leap year")]
        [TestCase("2026-04-31T12:00:00", TestName = "Day 31 in 30-day month")]
        [TestCase("2026-01-32T12:00:00", TestName = "Day 32")]
        [TestCase("2026-01-00T12:00:00", TestName = "Day zero")]
        [TestCase("2026-13-15T12:00:00", TestName = "Month 13")]
        [TestCase("2026-00-15T12:00:00", TestName = "Month zero")]
        [TestCase("2026-06-15T24:00:00", TestName = "Hour 24")]
        [TestCase("2026-06-15T12:60:00", TestName = "Minute 60")]
        [TestCase("2026-06-15T12:00:60", TestName = "Second 60")]
        [TestCase("2026-10-22 14:30:45", TestName = "Space instead of T")]
        [TestCase("2026-10-22t14:30:45", TestName = "Lowercase t")]
        [TestCase("2026/10/22T14:30:45", TestName = "Wrong date separator")]
        [TestCase("2026-10-22T14-30-45", TestName = "Wrong time separator")]
        [TestCase("22-10-2026T14:30:45", TestName = "Day first")]
        [TestCase("26-10-22T14:30:45", TestName = "Two-digit year")]
        [TestCase("2026-10-22T14:30", TestName = "Missing seconds")]
        [TestCase("2026-10-22", TestName = "Date only")]
        [TestCase("2026-10-22T14:30:45Z", TestName = "Trailing Z")]
        [TestCase("2026-10-22T14:30:45.123", TestName = "Fractional seconds")]
        [TestCase("2026-10-22T14:30:45+03:00", TestName = "Timezone offset")]
        [TestCase("2026-1-2T14:30:45", TestName = "Single-digit month and day")]
        [TestCase("abcd-ef-ghTij:kl:mn", TestName = "Letters")]
        [TestCase(" 2026-10-22T14:30:45", TestName = "Leading space")]
        [TestCase("2026-10-22T14:30:45 ", TestName = "Trailing space")]
        [TestCase("", TestName = "Empty string")]
        [TestCase("   ", TestName = "Whitespace only")]
        [TestCase(null, TestName = "Null")]
        public void IsDateTimeValidFormat_InvalidInput_ReturnsFalse(string? input)
        {
            bool isValid = _validation.IsDateTimeValidFormat(input!, out _);

            Assert.That(isValid, Is.False);
        }

        [TestCase("2026-10-22T14:00:00", "2026-10-22T15:30:00", TestName = "Same day, end later")]
        [TestCase("2026-10-22T23:00:00", "2026-10-23T01:00:00", TestName = "Crosses midnight")]
        [TestCase("2026-12-31T23:59:59", "2027-01-01T00:00:01", TestName = "Crosses year boundary")]
        public void AreDatesChronological_ValidInput_ReturnsTrue(string start, string end)
        {
            Assert.That(_validation.AreDatesChronological(Parse(start), Parse(end)), Is.True);
        }

        [TestCase("2026-10-22T15:30:00", "2026-10-22T14:00:00", TestName = "End before start")]
        [TestCase("2026-10-23T01:00:00", "2026-10-22T23:00:00", TestName = "End on previous day")]
        [TestCase("2026-10-22T14:00:00", "2026-10-22T14:00:00", TestName = "Equal times")]
        public void AreDatesChronological_EndNotAfterStart_ReturnsFalse(string start, string end)
        {
            Assert.That(_validation.AreDatesChronological(Parse(start), Parse(end)), Is.False);
        }
    }
}