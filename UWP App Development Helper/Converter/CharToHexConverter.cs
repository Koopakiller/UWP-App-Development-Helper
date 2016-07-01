using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.Converter
{
    public class CharToHexConverter : IValueConverter
    {
        //TODO: improve handling of wrong values
        public string Convert(char value) => this.Convert(value, null, null, "").ToString();
        public char ConvertBack(string value) => (char)this.ConvertBack(value, null, null, "");

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var chr = value as char?;
            if (chr == null)
            {
                return DependencyProperty.UnsetValue;
            }
            return System.Convert.ToString((int)chr, 16);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var s = value as string;
            if (s == null)
            {
                return DependencyProperty.UnsetValue;
            }
            return (char)System.Convert.ToInt32(s, 16);
        }
    }
}
