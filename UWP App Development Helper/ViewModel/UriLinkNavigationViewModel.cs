using System;
using System.Threading.Tasks;
using Windows.System;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class UriLinkNavigationViewModel : NavigationViewModelBase
    {
        public Uri Uri { get; set; }

        public override async Task NavigateAsync()
        {
            await Launcher.LaunchUriAsync(this.Uri);
        }

        public override Type GetTargetViewModelType()
        {
            return null;
        }
    }
}
