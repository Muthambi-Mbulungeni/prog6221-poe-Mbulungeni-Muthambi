using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ChatBotCyber.Views
{
    public partial class ToastNotification : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ToastNotification),
            new PropertyMetadata("Notification"));

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ToastNotification),
            new PropertyMetadata(""));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public ToastNotification()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ToastNotification(string title, string message) : this()
        {
            Title = title ?? string.Empty;
            Message = message ?? string.Empty;
        }

        public void Show()
        {
            if (Application.Current?.MainWindow == null) return;

            var popup = new Popup
            {
                Child = this,
                Placement = PlacementMode.Bottom,
                PlacementTarget = Application.Current.MainWindow,
                IsOpen = true,
                StaysOpen = false,
                HorizontalOffset = 20,
                VerticalOffset = -20
            };

            var timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };

            timer.Tick += (sender, e) =>
            {
                popup.IsOpen = false;
                timer.Stop();
            };

            timer.Start();
        }
    }
}