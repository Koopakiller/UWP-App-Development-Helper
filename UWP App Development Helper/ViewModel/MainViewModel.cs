using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _selectedContentViewModel;

        public MainViewModel()
        {
            Instance = this;
            this.SelectedContentViewModel = HomeViewModel;
            this.UpperHamburgerMenuItems = new ObservableCollection<HamburgerMenuItemViewModel>()
            {
                new HamburgerMenuItemViewModel()
                {
                    Header = "Home",
                    Glyph = "\uE10F",
                    ViewModel = this.SelectedContentViewModel,
                },
                new HamburgerMenuItemViewModel()
                {
                    Header = "Custom Colors",
                    Glyph = "\uE2B1",
                    ViewModel = CustomColorsViewModel,
                },
                new HamburgerMenuItemViewModel()
                {
                    Header = "Font Icons",
                    Glyph = "\uE128",
                    ViewModel = FontIconViewModel,
                },
            };
            this.LowerHamburgerMenuItems = new ObservableCollection<HamburgerMenuItemViewModel>()
            {
                new HamburgerMenuItemViewModel()
                {
                    Header = "About",
                    Glyph = "\uE946",
                    ViewModel = AboutViewModel,
                },
            };
        }

        public IList<HamburgerMenuItemViewModel> UpperHamburgerMenuItems { get; }
        public IList<HamburgerMenuItemViewModel> LowerHamburgerMenuItems { get; }

        public ViewModelBase SelectedContentViewModel
        {
            get { return this._selectedContentViewModel; }
            set
            {
                this._selectedContentViewModel = value;
                this.RaisePropertyChanged();
            }
        }

        public void Navigate(ViewModelBase vm)
        {
            this.SelectedContentViewModel = vm;
        }


        public static MainViewModel Instance { get; private set; }
        public static HomeViewModel HomeViewModel { get; } = new HomeViewModel();
        public static CustomColorsViewModel CustomColorsViewModel { get; } = new CustomColorsViewModel();
        public static FontIconViewModel FontIconViewModel { get; } = new FontIconViewModel();
        public static AboutViewModel AboutViewModel { get; } = new AboutViewModel();
    }
}
