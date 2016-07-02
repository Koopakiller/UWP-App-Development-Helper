using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Controls;
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
                    IconSource = new GlyphIconSource("\uE10F"),
                    ViewModelGenerator = () => new HomeViewModel(),
                    ViewModelType = typeof(HomeViewModel),
                },
                new ViewModelLinkViewModel()
                {
                    Header = "Colors",
                    IconSource = new GlyphIconSource("\uE2B1"),
                    ViewModelGenerator = () => new ColorsViewModel(),
                    ViewModelType = typeof(ColorsViewModel),
                },
                new ViewModelLinkViewModel()
                {
                    Header = "Font Icons",
                    IconSource = new GlyphIconSource("\uE128"),
                    ViewModelGenerator = () => new FontIconViewModel(),
                    ViewModelType = typeof(FontIconViewModel),
                },
            };
            this.LowerHamburgerMenuItems = new ObservableCollection<ViewModelLinkViewModel>()
            {
                new ViewModelLinkViewModel()
                {
                    Header = "Source on GitHub",
                    IconSource =new PathDataIconSource(@"M256,55.083c-113.764,0-206,92.237-206,206
	                                                     c0,91.013,59.025,168.246,140.887,195.472c10.293,1.911,13.613-4.459,13.613-9.908v-38.356C147.199,420.764,135.262,384,135.262,384
	                                                     c-9.354-23.822-22.865-30.159-22.865-30.159c-18.693-12.774,1.408-12.523,1.408-12.523c20.688,1.459,31.584,21.24,31.584,21.24
	                                                     c18.373,31.483,48.18,22.381,59.949,17.117c1.844-13.312,7.174-22.397,13.076-27.544c-45.768-5.197-93.848-22.867-93.848-101.81
	                                                     c0-22.498,8.047-40.872,21.225-55.289c-2.146-5.197-9.188-26.152,1.979-54.518c0,0,17.301-5.532,56.662,21.123
	                                                     c16.445-4.56,34.066-6.856,51.568-6.94c17.502,0.084,35.154,2.381,51.6,6.94c39.33-26.655,56.596-21.123,56.596-21.123
	                                                     c11.217,28.365,4.158,49.32,2.029,54.518c13.211,14.417,21.207,32.791,21.207,55.289c0,79.127-48.197,96.545-94.064,101.642
	                                                     c7.375,6.388,14.133,18.943,14.133,38.155c0,27.561,0,49.757,0,56.529c0,5.482,3.301,11.903,13.746,9.892
	                                                     C403.057,429.264,462,352.08,462,261.084C462,147.321,369.762,55.083,256,55.083z")
                    //ViewModelGenerator = ()=>new AboutViewModel(),
                    //ViewModelType = typeof(AboutViewModel),
                },
                new ViewModelLinkViewModel()
                {
                    Header = "About",
                    IconSource =new GlyphIconSource( "\uE946"),
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
                    var item = this.UpperHamburgerMenuItems.Concat(this.LowerHamburgerMenuItems).FirstOrDefault(x => x.ViewModelType == value.GetType());
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
