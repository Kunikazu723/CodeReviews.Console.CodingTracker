namespace CodingTracker.Kunikazu723.Models
{
    public class CodingSession
    {
        //Id, StartTime, EndTime, Duration
        public int Id { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public float Duration { get; set; }
    }
}
