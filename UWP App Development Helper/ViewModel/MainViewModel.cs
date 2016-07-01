using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Helper;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _selectedContentViewModel;
        private ViewModelLinkViewModel _selectedHamburgerItem;

        public MainViewModel()
        {
            NavigationHelper.MainViewModel = this;
            this.UpperHamburgerMenuItems = new ObservableCollection<ViewModelLinkViewModel>()
            {
                new ViewModelLinkViewModel()
                {
                    Header = "Home",
                    Glyph = "\uE10F",
                    ViewModelGenerator = () => new HomeViewModel(),
                    ViewModelType = typeof(HomeViewModel),
                },
                new ViewModelLinkViewModel()
                {
                    Header = "Colors",
                    Glyph = "\uE2B1",
                    ViewModelGenerator = () => new ColorsViewModel(),
                    ViewModelType = typeof(ColorsViewModel),
                },
                new ViewModelLinkViewModel()
                {
                    Header = "Font Icons",
                    Glyph = "\uE128",
                    ViewModelGenerator = () => new FontIconViewModel(),
                    ViewModelType = typeof(FontIconViewModel),
                },
            };
            this.LowerHamburgerMenuItems = new ObservableCollection<ViewModelLinkViewModel>()
            {
                new ViewModelLinkViewModel()
                {
                    Header = "About",
                    Glyph = "\uE946",
                    ViewModelGenerator = ()=>new AboutViewModel(),
                    ViewModelType = typeof(AboutViewModel),
                },
            };
            this.SelectedHamburgerItem = this.UpperHamburgerMenuItems.FirstOrDefault();
        }

        public IList<ViewModelLinkViewModel> UpperHamburgerMenuItems { get; }
        public IList<ViewModelLinkViewModel> LowerHamburgerMenuItems { get; }
        
        public ViewModelLinkViewModel SelectedHamburgerItem
        {
            get { return this._selectedHamburgerItem; }
            set
            {
                this._selectedHamburgerItem = value;
                if (value != null)
                {
                    this.SelectedContentViewModel = value.ViewModelGenerator();
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
                if (value != null)
                {
                    var item= this.UpperHamburgerMenuItems.Concat(this.LowerHamburgerMenuItems).FirstOrDefault(x => x.ViewModelType == value.GetType());
                    if (this.SelectedHamburgerItem != item)
                    {
                        this._selectedHamburgerItem = item;
                        // ReSharper disable once ExplicitCallerInfoArgument
                        this.RaisePropertyChanged(nameof(this.SelectedHamburgerItem));
                    }
                }
                this.RaisePropertyChanged();
            }
        }

    }
}
