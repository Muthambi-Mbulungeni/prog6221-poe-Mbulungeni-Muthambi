using ChatBotCyber.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ChatBotCyber.Services
{
    public class BotService
    {
        private readonly TopicSearch _topicSearch = new();
        private readonly TaskManager _taskManager = TaskManager.Instance;
        private readonly QuizManager _quizManager = new();
        private readonly ActivityLogger _activityLogger = new();
        private QuizState? _quizState;

        public List<string> GetAllTopics() => _topicSearch.GetAllTopics();

        public string GetHelpText()
        {
            return @"Available commands:
- Topics: Ask about cybersecurity topics (phishing, malware, etc.)
- 'Add task [title]': Create a new cybersecurity task
- 'Start quiz': Begin cybersecurity quiz
- 'Show tasks': View your tasks
- 'Activity log': View recent actions
- 'Help': Show this help message";
        }

        public string StartQuiz()
        {
            _quizState = new QuizState(_quizManager);
            _activityLogger.Log("Started cybersecurity quiz");
            return _quizState.GetNextQuestion() ?? "Quiz failed to start. Please try again.";
        }

        public string ProcessInput(string input, string userName, string userFavoriteTopic)
        {
            input ??= string.Empty;
            userName ??= string.Empty;

            if (_quizState?.IsActive == true)
                return ProcessQuizAnswer(input) ?? string.Empty;

            if (CommandParser.TryParse(input, out string? commandType))
            {
                return HandleCommand(input, commandType, userName) ?? string.Empty;
            }

            return _topicSearch.SearchTopic(input, userName, userFavoriteTopic) ?? string.Empty;
        }

        private string? HandleCommand(string input, string? commandType, string userName)
        {
            if (commandType == null) return null;

            switch (commandType)
            {
                case "task":
                    return HandleTaskCommand(input);
                case "reminder":
                    return HandleReminderCommand(input);
                case "quiz":
                    return StartQuiz();
                case "log":
                    return _activityLogger.GetRecentLogs();
                case "tasks":
                    return GetTasksList();
                case "help":
                    return GetHelpText();
                default:
                    return "I'm not sure how to handle that request.";
            }
        }

        private string HandleTaskCommand(string input)
        {
            string title = ExtractPhraseAfter(input, "add task", "create task") ?? string.Empty;
            if (string.IsNullOrEmpty(title))
                return "Please specify a task title";

            var task = new TaskItem { Title = title };
            _taskManager.AddTask(task);
            _activityLogger.Log($"Task added: {title}");

            return $"Task '{title}' added. Would you like to set a reminder? (Yes/No)";
        }

        private string ProcessQuizAnswer(string input)
        {
            if (_quizState == null) return "Quiz not started";

            string result = _quizState.AnswerQuestion(input) ?? string.Empty;
            _activityLogger.Log($"Answered quiz question: {input}");

            if (_quizState.IsComplete)
            {
                _activityLogger.Log($"Quiz completed. Score: {_quizState.Score}/10");
                return $"{result}\nFinal Score: {_quizState.Score}/10\n{GetQuizFeedback(_quizState.Score)}";
            }

            return result;
        }

        private string GetQuizFeedback(int score)
        {
            return score switch
            {
                >= 8 => "Excellent! You're a cybersecurity expert!",
                >= 5 => "Good job! You have solid cybersecurity knowledge.",
                _ => "Keep learning! Cybersecurity is important for everyone."
            };
        }

        private string GetTasksList()
        {
            var tasks = _taskManager.Tasks;
            if (tasks.Count == 0) return "You have no pending cybersecurity tasks.";

            var sb = new StringBuilder();
            sb.AppendLine("Your cybersecurity tasks:");
            foreach (var task in tasks)
            {
                sb.AppendLine($"- {task.Title} {(task.IsCompleted ? "(Completed)" : "")}");
                if (!string.IsNullOrEmpty(task.Description))
                    sb.AppendLine($"  Description: {task.Description}");
                if (task.ReminderTime.HasValue)
                    sb.AppendLine($"  Reminder: {task.ReminderTime.Value:g}");
            }
            return sb.ToString();
        }

        private string? ExtractPhraseAfter(string input, params string[] triggers)
        {
            if (string.IsNullOrEmpty(input)) return null;

            string lowerInput = input.ToLower();
            foreach (string trigger in triggers)
            {
                int index = lowerInput.IndexOf(trigger);
                if (index >= 0)
                {
                    return input.Substring(index + trigger.Length).Trim();
                }
            }
            return null;
        }

        private string HandleReminderCommand(string input)
        {
            return "Reminder functionality is automatically handled when you add tasks with reminders.";
        }
    }
}