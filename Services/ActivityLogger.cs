using ChatBotCyber.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatBotCyber.Services
{
    public class ActivityLogger
    {
        private readonly Queue<ActivityLogItem> _logEntries = new Queue<ActivityLogItem>(10);

        public void Log(string activity)
        {
            if (_logEntries.Count >= 10)
            {
                _logEntries.Dequeue();
            }

            _logEntries.Enqueue(new ActivityLogItem(activity));
        }

        public string GetRecentLogs()
        {
            return "Recent Activities:\n" +
                   string.Join("\n", _logEntries.Reverse().Select(item => item.ToString()));
        }
    }
}