using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.Controls
{
    public class Plaid : UserControl
    {
        public Plaid()
        {
            var cc = new CanvasControl();
            cc.Draw += this.OnDraw;
            this.Content = cc;
        }

        protected virtual void OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            var cx = (float)(this.HorizontalMode == PlaidMode.Count ? this.ActualWidth / this.Horizontal : this.Horizontal);
            var cy = (float)(this.VerticalMode == PlaidMode.Count ? this.ActualHeight / this.Horizontal : this.Horizontal);

            for (var x = 0; x * cx < this.ActualWidth; ++x)
            {
                for (var y = 0; y * cy < this.ActualHeight; ++y)
                {
                    args.DrawingSession.FillRectangle(x * cx, y * cy, cx, cy, x % 2 == y % 2 ? this.FirstColor : this.SecondColor);
                }
            }
        }

        #region Properties

        public Color FirstColor
        {
            get { return (Color)this.GetValue(FirstColorProperty); }
            set { this.SetValue(FirstColorProperty, value); }
        }

        public Color SecondColor
        {
            get { return (Color)this.GetValue(SecondColorProperty); }
            set { this.SetValue(SecondColorProperty, value); }
        }

        public double Horizontal
        {
            get { return (double)this.GetValue(HorizontalProperty); }
            set { this.SetValue(HorizontalProperty, value); }
        }

        public double Vertical
        {
            get { return (double)this.GetValue(VerticalProperty); }
            set { this.SetValue(VerticalProperty, value); }
        }

        public PlaidMode HorizontalMode
        {
            get { return (PlaidMode)this.GetValue(HorizontalModeProperty); }
            set { this.SetValue(HorizontalModeProperty, value); }
        }

        public PlaidMode VerticalMode
        {
            get { return (PlaidMode)this.GetValue(VerticalModeProperty); }
            set { this.SetValue(VerticalModeProperty, value); }
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty FirstColorProperty = DependencyProperty.Register(nameof(FirstColor), typeof(Color), typeof(Plaid), new PropertyMetadata(Colors.Black));
        public static readonly DependencyProperty SecondColorProperty = DependencyProperty.Register(nameof(SecondColor), typeof(Color), typeof(Plaid), new PropertyMetadata(Colors.White));
        public static readonly DependencyProperty HorizontalProperty = DependencyProperty.Register(nameof(Horizontal), typeof(double), typeof(Plaid), new PropertyMetadata(25D));
        public static readonly DependencyProperty VerticalProperty = DependencyProperty.Register(nameof(Vertical), typeof(double), typeof(Plaid), new PropertyMetadata(25D));
        public static readonly DependencyProperty HorizontalModeProperty = DependencyProperty.Register(nameof(HorizontalMode), typeof(PlaidMode), typeof(Plaid), new PropertyMetadata(PlaidMode.Count));
        public static readonly DependencyProperty VerticalModeProperty = DependencyProperty.Register(nameof(VerticalMode), typeof(PlaidMode), typeof(Plaid), new PropertyMetadata(PlaidMode.Count));

        #endregion

    }

    public enum PlaidMode
    {
        Count,
        Pixel,
    }
}
