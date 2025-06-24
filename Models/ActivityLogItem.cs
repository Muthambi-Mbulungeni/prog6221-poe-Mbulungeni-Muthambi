namespace ChatBotCyber.Models
{
    public class ActivityLogItem
    {
        public DateTime Timestamp { get; set; }
        public string Activity { get; set; } = string.Empty;  // Initialize with empty string

        public ActivityLogItem(string activity)
        {
            Timestamp = DateTime.Now;
            Activity = activity ?? string.Empty;  // Handle null
        }

        public override string ToString()
        {
            return $"[{Timestamp:HH:mm:ss}] {Activity}";
        }
    }
}