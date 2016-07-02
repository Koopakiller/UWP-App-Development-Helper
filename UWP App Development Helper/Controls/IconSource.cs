using Windows.UI.Xaml;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.Controls
{
    public abstract class IconSource : DependencyObject
    {
        public abstract FrameworkElement CreateControl(double width, double height);
        public abstract void UpdateControlSize(double width, double height);
    }

    public abstract class IconSource<T> : IconSource
    {
        protected IconSource(T source)
        {
            this.Source = source;
        }

        public T Source
        {
            get { return (T)this.GetValue(SourceProperty); }
            set { this.SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(T), typeof(IconSource<T>), new PropertyMetadata(default(T)));
    }
}