namespace ChatBotCyber.Services
{
    public static class CommandParser
    {
        private static readonly Dictionary<string, string> CommandMap = new()
        {
            { "add task", "task" },
            { "create task", "task" },
            { "new task", "task" },
            { "remind me", "reminder" },
            { "set reminder", "reminder" },
            { "start quiz", "quiz" },
            { "begin game", "quiz" },
            { "activity log", "log" },
            { "what have you done", "log" },
            { "show history", "log" },
            { "show tasks", "tasks" },
            { "view tasks", "tasks" },
            { "my tasks", "tasks" },
            { "help", "help" },
            { "commands", "help" },
            { "what can i do", "help" }
        };

        public static bool TryParse(string input, out string? commandType)
        {
            commandType = null;
            if (string.IsNullOrEmpty(input)) return false;

            string lowerInput = input.ToLower();

            foreach (var kvp in CommandMap)
            {
                if (lowerInput.Contains(kvp.Key))
                {
                    commandType = kvp.Value;
                    return true;
                }
            }

            return false;
        }
    }
}