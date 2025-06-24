using ChatBotCyber.Models;
using ChatBotCyber.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ChatBotCyber.Views
{
    public partial class TaskWindow : Window
    {
        private readonly TaskManager _taskManager = TaskManager.Instance;

        public TaskWindow()
        {
            InitializeComponent();
            TasksListView.ItemsSource = _taskManager.Tasks;
            PopulateTimeComboBoxes();
        }

        private void PopulateTimeComboBoxes()
        {
            // Clear existing items
            HourCombo.Items.Clear();
            MinuteCombo.Items.Clear();

            // Add hours as strings
            for (int hour = 0; hour < 24; hour++)
            {
                HourCombo.Items.Add(hour.ToString("00"));
            }

            // Add minutes as strings
            for (int minute = 0; minute < 60; minute += 5)
            {
                MinuteCombo.Items.Add(minute.ToString("00"));
            }

            // Set default selections
            HourCombo.SelectedIndex = 12; // 12 PM
            MinuteCombo.SelectedIndex = 0; // :00
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                MessageBox.Show("Please enter a task title");
                return;
            }

            DateTime? reminderTime = null;
            if (DatePicker.SelectedDate.HasValue &&
                HourCombo.SelectedItem != null &&
                MinuteCombo.SelectedItem != null)
            {
                // Directly use the string values
                string hourString = HourCombo.SelectedItem.ToString();
                string minuteString = MinuteCombo.SelectedItem.ToString();

                if (int.TryParse(hourString, out int hour) &&
                    int.TryParse(minuteString, out int minute))
                {
                    var date = DatePicker.SelectedDate.Value;
                    var time = new TimeSpan(hour, minute, 0);
                    reminderTime = date + time;
                }
            }

            var task = new TaskItem
            {
                Title = TitleBox.Text,
                Description = DescBox.Text,
                ReminderTime = reminderTime
            };

            _taskManager.AddTask(task);

            // Clear form
            TitleBox.Clear();
            DescBox.Clear();
            DatePicker.SelectedDate = null;
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag is TaskItem task)
            {
                _taskManager.RemoveTask(task);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}