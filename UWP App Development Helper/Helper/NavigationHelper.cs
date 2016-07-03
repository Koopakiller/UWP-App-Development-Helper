using System;
using System.Linq;
using Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.Helper
{
    public static class NavigationHelper
    {
        public static MainViewModel MainViewModel { get; set; }

        public static void NavigateToExisting(ViewModelBase vm)
        {
            MainViewModel.SelectedContentViewModel = vm;
        }
        
        public static void NavigateToExisting(Type type)
        {
            var target = MainViewModel.UpperHamburgerMenuItems.Concat(MainViewModel.LowerHamburgerMenuItems)
                                                              .FirstOrDefault(x => (x as ViewModelNavigationViewModel)?.TargetViewModelType == type);
            MainViewModel.SelectedHamburgerItem = target;
        }
    }
}
