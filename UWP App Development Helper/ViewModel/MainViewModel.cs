using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public static MainViewModel Instance { get;private set; }

        private ViewModelBase _selectedContentViewModel;

        public MainViewModel()
        {
            Instance = this;
            this.SelectedContentViewModel = new HomeViewModel();
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
    }
}
