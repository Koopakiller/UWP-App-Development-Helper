using Windows.UI;
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

        public SolidColorBrush Brush { get; }
        public Color Color { get; }
        public string DisplayName { get; }
    }
}