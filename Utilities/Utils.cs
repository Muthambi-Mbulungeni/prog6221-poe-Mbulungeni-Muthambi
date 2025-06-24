namespace ChatBotCyber.Utilities
{
    public static class Utils
    {
        public static string MakeTextBold(string text) => $"\u001b[1m{text}\u001b[0m";
        public static string MakeTextGreen(string text) => $"\u001b[32m{text}\u001b[0m";
    }
}