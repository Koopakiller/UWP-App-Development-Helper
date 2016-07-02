using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.Controls
{
    public class Icon : ContentControl
    {
        public Icon()
        {
            this.HorizontalContentAlignment = HorizontalAlignment.Center;
            this.VerticalContentAlignment = VerticalAlignment.Center;
            this.SizeChanged += this.OnSizeChanged;
        }

        public IconSource Source
        {
            get { return (IconSource)this.GetValue(SourceProperty); }
            set { this.SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(/*IconSource*/object), typeof(Icon), new PropertyMetadata(null, OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var icon = (Icon)d;
            var value = (IconSource)e.NewValue;
            icon.Content = value.CreateControl(icon.ActualWidth, icon.ActualHeight);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Source?.UpdateControlSize(this.ActualWidth, this.ActualHeight);
        }

    }
}
