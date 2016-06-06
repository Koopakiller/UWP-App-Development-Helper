using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class ColorViewModel : ViewModelBase
    {
        public ColorViewModel()
        {
        }

        public ColorViewModel(byte a, byte r, byte g, byte b)
        {
            this.Color = Color.FromArgb(a, r, g, b);
            this.Brush = new SolidColorBrush(this.Color);

            this.DisplayName = $"{r:X2}{g:X2}{b:X2}";
            if (a != 0xFF)
            {
                this.DisplayName = $"{a:X2}{this.DisplayName}";
            }
        }

        public ColorViewModel(string resourceKey)
        {
            object res ;
            try
            {
                res = Application.Current.Resources[resourceKey];
            }
            catch
            {
                //No resource found or an error occured
                return;
            }
            if (res is Color)
            {
                this.Color = (Color)res;
                this.Brush = new SolidColorBrush(this.Color);
            }
            else if (res is Brush)
            {
                this.Brush = (Brush)res;
                var solidColorBrush = res as SolidColorBrush;
                if (solidColorBrush != null)
                {
                    this.Color = solidColorBrush.Color;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
            this.DisplayName = resourceKey;
        }

        public Brush Brush { get; }
        public Color Color { get; }
        public string DisplayName { get; set; }
    }
}