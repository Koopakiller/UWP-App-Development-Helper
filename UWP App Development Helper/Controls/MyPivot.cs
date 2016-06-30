using Windows.UI.Xaml.Controls;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.Controls
{
    public class MyPivot : Pivot
    {
        public MyPivot()
        {
            this.SelectionChanged += this.OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var width = this.Width;
            this.Width = double.IsInfinity(width) || double.IsNaN(width) ? 100 : width + 1;
            this.UpdateLayout();
            this.Width = width;
        }
    }
}
