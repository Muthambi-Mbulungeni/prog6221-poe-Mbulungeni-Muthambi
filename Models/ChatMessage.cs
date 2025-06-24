namespace ChatBotCyber.Models
{
    public class ChatMessage
    {
        public string Sender { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public ChatMessage() { }

        public ChatMessage(string sender, string content)
        {
            Sender = sender ?? string.Empty;
            Content = content ?? string.Empty;
        }
    }
}