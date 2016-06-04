using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.Converter
{
    public class BooleanToVisibilityConverter : DependencyObject, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var b = value as bool?;
            if (b == null)
            {
                return DependencyProperty.UnsetValue;
            }

            return b != this.Negate ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var b = value as Visibility?;
            if (b == null)
            {
                return DependencyProperty.UnsetValue;
            }

            return (b == Visibility.Visible) != this.Negate;
        }



        public bool Negate
        {
            get { return (bool)this.GetValue(NegateProperty); }
            set { this.SetValue(NegateProperty, value); }
        }

        public static readonly DependencyProperty NegateProperty = DependencyProperty.Register(nameof(Negate), typeof(bool), typeof(BooleanToVisibilityConverter), new PropertyMetadata(false));


    }
}
