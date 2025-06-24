namespace ChatBotCyber.Models
{
    public class TaskItem
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? ReminderTime { get; set; }
        public bool IsCompleted { get; set; }
    }
}