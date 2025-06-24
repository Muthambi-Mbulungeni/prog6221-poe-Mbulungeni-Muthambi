using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ChatBotCyber.Converters
{
    public class SenderBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string sender)
            {
                return sender == "You"
                    ? new SolidColorBrush(Color.FromRgb(30, 144, 255))
                    : new SolidColorBrush(Color.FromRgb(50, 50, 50));
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}