using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Helper;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Model;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {
            this.NavigateCommand = new RelayCommand<ItemClickEventArgs>(this.Navigate);
            this.HistoryNavigateCommand = new RelayCommand<ItemClickEventArgs>(this.HistoryNavigate);
            this.Items = new ObservableCollection<ViewModelLinkViewModel>()
            {
                new ViewModelLinkViewModel()
                {
                    Header = "Accent Colors",
                    Glyph = "\uE2B1",
                    ViewModelGenerator = () =>
                    {
                        var cvm = new ColorsViewModel();
                        cvm.SelectedSubSection = cvm.SubSections.FirstOrDefault(x => x.GetType() == typeof (AccentColorsViewModel));
                        return cvm;
                    },
                    ViewModelType = typeof(ColorsViewModel),
                },
                new ViewModelLinkViewModel()
                {
                    Header = "ThemeResource Colors",
                    Glyph = "\uE2B1",
                    ViewModelGenerator = () =>
                    {
                        var cvm = new ColorsViewModel();
                        cvm.SelectedSubSection = cvm.SubSections.FirstOrDefault(x => x.GetType() == typeof (ThemeResourceColorViewModel));
                        return cvm;
                    },
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
        }

        public ObservableCollection<ViewModelLinkViewModel> Items { get; }

        public ICommand NavigateCommand { get; }
        public ICommand HistoryNavigateCommand { get; }

        private void Navigate(ItemClickEventArgs e)
        {
            var item = (ViewModelLinkViewModel)e.ClickedItem;
            NavigationHelper.NavigateToExisting(item.ViewModelGenerator());
        }

        private void HistoryNavigate(ItemClickEventArgs e)
        {
            var item = (ViewModelBase)(IHistoryItemTarget)e.ClickedItem;
            NavigationHelper.NavigateToExisting(item);
        }

        public HistoryProvider History { get; } = HistoryProvider.Instance;
    }
}
