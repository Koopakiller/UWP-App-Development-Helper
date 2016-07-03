using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Threading;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Helper;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class ViewModelNavigationViewModel : NavigationViewModelBase
    {
        public Type TargetViewModelType { get; set; }

        public Func<ViewModelBase> ViewModelGenerator { get; set; }

        public override async Task NavigateAsync()
        {
            await DispatcherHelper.RunAsync(() =>
            {
                NavigationHelper.NavigateToExisting(this.ViewModelGenerator());
            });
        }

        public override Type GetTargetViewModelType()
        {
            return this.TargetViewModelType;
        }
    }
}