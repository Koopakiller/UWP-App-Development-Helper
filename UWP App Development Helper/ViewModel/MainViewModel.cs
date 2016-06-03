using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            this.HamburgerMenuItems = new ObservableCollection<HamburgerMenuItemViewModel>()
            {
                new HamburgerMenuItemViewModel()
                {
                    Header = "Home",
                    Glyph = "\uE10F",
                    ViewModel = new HomeViewModel(),
                },
                new HamburgerMenuItemViewModel()
                {
                    Header = "Custom Colors",
                    Glyph = "\uE2B1",
                    ViewModel = new CustomColorsViewModel(),
                },
                new HamburgerMenuItemViewModel()
                {
                    Header = "Font Icons",
                    Glyph = "\uE128",
                    ViewModel = new FontIconViewModel(),
                },
                new HamburgerMenuItemViewModel()
                {
                    Header = "About",
                    Glyph = "\uE946",
                    ViewModel = new AboutViewModel(),
                },
            };
        }

        public IList<HamburgerMenuItemViewModel> HamburgerMenuItems { get; }
    }
}
