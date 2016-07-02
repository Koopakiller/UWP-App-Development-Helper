using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Shapes;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.Controls
{
    public class PathDataIconSource : IconSource<string>
    {
        public PathDataIconSource(string source) : base(source) { }

        private Path _path;

        public override FrameworkElement CreateControl(double width, double height)
        {
            return this._path = (Path)XamlReader.Load($@"<Path Data=""{this.Source}"" Width=""{width}"" Height=""{height}"" />");
        }

        public override void UpdateControlSize(double width, double height)
        {
            //_path...
        }
    }
}