using System.Reflection;
using Windows.UI.Xaml.Controls;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.View
{
    public class ViewBase : Page
    {
        public ViewBase()
        {
            var method = this.GetType().GetMethod("InitializeComponent");
            method?.Invoke(this, new object[0]);
        }
    }
}
