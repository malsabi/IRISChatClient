using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace IRISChatClient.Converters
{
    public class BoolToGenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (string)parameter == (string)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? parameter : DependencyProperty.UnsetValue;
        }
    }
}