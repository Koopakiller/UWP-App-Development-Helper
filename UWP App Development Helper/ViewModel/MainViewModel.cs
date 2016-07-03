using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Controls;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Helper;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _selectedContentViewModel;
        private NavigationViewModelBase _selectedHamburgerItem;

        public MainViewModel()
        {
            this.MenuItemSelectedCommand = new RelayCommand<ItemClickEventArgs>(this.OnMenuItemSelected);
            NavigationHelper.MainViewModel = this;
            this.LoadMenuItems();
        }

        private async void LoadMenuItems()
        {
            this.UpperHamburgerMenuItems = new ObservableCollection<NavigationViewModelBase>()
            {
                new ViewModelNavigationViewModel()
                {
                    Header = "Home",
                    IconSource = new GlyphIconSource("\uE10F"),
                    ViewModelGenerator = () => new HomeViewModel(),
                    TargetViewModelType = typeof(HomeViewModel),
                },
                new ViewModelNavigationViewModel()
                {
                    Header = "Colors",
                    IconSource = new GlyphIconSource("\uE2B1"),
                    ViewModelGenerator = () => new ColorsViewModel(),
                    TargetViewModelType = typeof(ColorsViewModel),
                },
                new ViewModelNavigationViewModel()
                {
                    Header = "Font Icons",
                    IconSource = new GlyphIconSource("\uE128"),
                    ViewModelGenerator = () => new FontIconViewModel(),
                    TargetViewModelType = typeof(FontIconViewModel),
                },
            };

            this.LowerHamburgerMenuItems = new ObservableCollection<NavigationViewModelBase>()
            {
                new UriLinkNavigationViewModel()
                {
                    Header = "Source on GitHub",
                    IconSource =await  PathDataIconSource.FromResourceAsync(@"PathData/GitHub.txt"),
                    Uri = new Uri(@"https://github.com/Koopakiller/UWP-App-Development-Helper"),
                },
                new ViewModelNavigationViewModel()
                {
                    Header = "About",
                    IconSource = new GlyphIconSource("\uE946"),
                    ViewModelGenerator = () => new AboutViewModel(),
                    TargetViewModelType = typeof (AboutViewModel),
                },
            };
            await this.UpperHamburgerMenuItems.FirstOrDefault().NavigateAsync();
        }

        public ICommand MenuItemSelectedCommand { get; }

        private void OnMenuItemSelected(ItemClickEventArgs e)
        {
            var item = (NavigationViewModelBase)e.ClickedItem;
            item.NavigateAsync();
        }

        public IList<NavigationViewModelBase> UpperHamburgerMenuItems { get; private set; }
        public IList<NavigationViewModelBase> LowerHamburgerMenuItems { get; private set; }

        public NavigationViewModelBase SelectedHamburgerItem
        {
            get { return this._selectedHamburgerItem; }
            set
            {
                if (value.GetTargetViewModelType() != null)
                {
                    this._selectedHamburgerItem = value;
                }
                this.RaisePropertyChanged();
            }
        }

        public ViewModelBase SelectedContentViewModel
        {
            get { return this._selectedContentViewModel; }
            set
            {
                this._selectedContentViewModel = value;
                this.RaisePropertyChanged();

                if (value != null)
                {
                    var item = this.UpperHamburgerMenuItems.Concat(this.LowerHamburgerMenuItems)
                                                           .FirstOrDefault(x => x.GetTargetViewModelType() == value.GetType());
                    if (this.SelectedHamburgerItem != item)
                    {
                        this._selectedHamburgerItem = item;
                        // ReSharper disable once ExplicitCallerInfoArgument
                        this.RaisePropertyChanged(nameof(this.SelectedHamburgerItem));
                    }
                }
                else
                {
                    this._selectedHamburgerItem = null;
                    // ReSharper disable once ExplicitCallerInfoArgument
                    this.RaisePropertyChanged(nameof(this.SelectedHamburgerItem));
                }
            }
        }

    }
}
