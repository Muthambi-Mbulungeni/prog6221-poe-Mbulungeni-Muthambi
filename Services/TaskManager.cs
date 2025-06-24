using ChatBotCyber.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows;

namespace ChatBotCyber.Services
{
    public class TaskManager
    {
        private static readonly TaskManager _instance = new TaskManager();
        public static TaskManager Instance => _instance;

        public ObservableCollection<TaskItem> Tasks { get; } = new ObservableCollection<TaskItem>();
        private readonly List<System.Timers.Timer> _timers = new List<System.Timers.Timer>();

        private TaskManager() { }

        public void AddTask(TaskItem task)
        {
            Tasks.Add(task);
            if (task.ReminderTime.HasValue)
            {
                ScheduleReminder(task);
            }
        }

        public void RemoveTask(TaskItem task)
        {
            Tasks.Remove(task);
        }

        private void ScheduleReminder(TaskItem task)
        {
            TimeSpan timeUntilReminder = task.ReminderTime.Value - DateTime.Now;
            if (timeUntilReminder.TotalMilliseconds <= 0) return;

            var timer = new System.Timers.Timer(timeUntilReminder.TotalMilliseconds)
            {
                AutoReset = false
            };

            timer.Elapsed += (sender, e) => ShowReminder(task);
            timer.Start();
            _timers.Add(timer);
        }

        private void ShowReminder(TaskItem task)
        {
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    new Views.ToastNotification($"Reminder: {task.Title}",
                        task.Description).Show();
                });
            }
        }
    }
}