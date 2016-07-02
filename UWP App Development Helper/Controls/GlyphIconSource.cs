using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.Controls
{
    public class GlyphIconSource : IconSource<string>
    {
        public double FontSize
        {
            get { return (double)this.GetValue(FontSizeProperty); }
            set { this.SetValue(FontSizeProperty, value); }
        }

        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(nameof(FontSize), typeof(double), typeof(GlyphIconSource), new PropertyMetadata(20D));

        public GlyphIconSource(string source) : base(source) { }

        private FontIcon _fontIcon;

        public override FrameworkElement CreateControl(double width, double height)
        {
            this._fontIcon = new FontIcon()
            {
                Glyph = this.Source,
            };
            this.UpdateControlSize(width, height);
            return this._fontIcon;
        }

        public override void UpdateControlSize(double width, double height)
        {
            this._fontIcon.FontSize = double.IsNaN(this.FontSize) ? Math.Max(Math.Min(width, height), 1) / 96D * 72D : this.FontSize;
        }
    }
}