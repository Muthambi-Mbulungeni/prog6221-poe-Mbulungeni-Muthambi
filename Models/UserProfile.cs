using System;

namespace ChatBotCyber.Models
{
    public class UserProfile
    {
        public string Name { get; set; } = string.Empty;
        public string FavoriteTopic { get; set; } = "cybersecurity";
        public DateTime LastSeen { get; set; } = DateTime.Now;
    }
}