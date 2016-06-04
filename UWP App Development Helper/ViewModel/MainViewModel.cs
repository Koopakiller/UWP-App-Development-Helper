using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private HamburgerMenuItemViewModel _selectedHamburgerMenuItem;

        public MainViewModel()
        {
            this.SelectedHamburgerMenuItem = new HamburgerMenuItemViewModel()
            {
                Header = "Home",
                Glyph = "\uE10F",
                ViewModel = new HomeViewModel(),
            };
            this.UpperHamburgerMenuItems = new ObservableCollection<HamburgerMenuItemViewModel>()
            {
                this.SelectedHamburgerMenuItem,
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
            };
            this.LowerHamburgerMenuItems = new ObservableCollection<HamburgerMenuItemViewModel>()
            {
                new HamburgerMenuItemViewModel()
                {
                    Header = "About",
                    Glyph = "\uE946",
                    ViewModel = new AboutViewModel(),
                },
            };
        }

        public IList<HamburgerMenuItemViewModel> UpperHamburgerMenuItems { get; }
        public IList<HamburgerMenuItemViewModel> LowerHamburgerMenuItems { get; }

        public HamburgerMenuItemViewModel SelectedHamburgerMenuItem
        {
            get { return this._selectedHamburgerMenuItem; }
            set
            {
                this._selectedHamburgerMenuItem = value; 
                this.RaisePropertyChanged();
            }
        }
    }
}
