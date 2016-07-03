using System;
using System.Threading.Tasks;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Controls;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public abstract class NavigationViewModelBase : ViewModelBase
    {
        public abstract Task NavigateAsync();

        public string Header { get; set; }

        public IconSource IconSource { get; set; }

        public abstract Type GetTargetViewModelType();
    }
}