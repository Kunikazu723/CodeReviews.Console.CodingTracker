namespace CodingTracker.Kunikazu723.Models
{
    public class CodingSession
    {
        //Id, StartTime, EndTime, Duration
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
