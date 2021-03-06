using System;
using System.Threading.Tasks;
using Windows.Storage;
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
            return this._path = (Path)XamlReader.Load(
                $@"<Path xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" 
                         Data=""{this.Source}"" 
                         Width=""{width}"" 
                         Height=""{height}"" 
                         Stretch=""Fill""
                         StrokeThickness=""0""
                         Fill=""Black"" />");
        }

        public override void UpdateControlSize(double width, double height)
        {
            this._path.Width = width;
            this._path.Height = height;
        }

        public static async Task<PathDataIconSource> FromStorageFileAsync(IStorageFile file)
        {
            return new PathDataIconSource(await FileIO.ReadTextAsync(file));
        }

        public static async Task<PathDataIconSource> FromResourceAsync(string filePath)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Resources{(filePath.StartsWith("/") ? "" : "/")}{filePath}"));
            return await FromStorageFileAsync(file);
        }
    }
}