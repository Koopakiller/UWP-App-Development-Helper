using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Controls;
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
            this.Items = new ObservableCollection<ViewModelNavigationViewModel>()
            {
                new ViewModelNavigationViewModel()
                {
                    Header = "Accent Colors",
                    IconSource = new GlyphIconSource("\uE2B1") {FontSize = double.NaN},
                    ViewModelGenerator = () =>
                    {
                        var cvm = new ColorsViewModel();
                        cvm.SelectedSubSection = cvm.SubSections.FirstOrDefault(x => x.GetType() == typeof (AccentColorsViewModel));
                        return cvm;
                    },
                    TargetViewModelType = typeof (ColorsViewModel),
                },
                new ViewModelNavigationViewModel()
                {
                    Header = "ThemeResource Colors",
                    IconSource = new GlyphIconSource("\uE2B1") {FontSize = double.NaN},
                    ViewModelGenerator = () =>
                    {
                        var cvm = new ColorsViewModel();
                        cvm.SelectedSubSection = cvm.SubSections.FirstOrDefault(x => x.GetType() == typeof (ThemeResourceColorViewModel));
                        return cvm;
                    },
                    TargetViewModelType = typeof (ColorsViewModel),
                },
                new ViewModelNavigationViewModel()
                {
                    Header = "Font Icons",
                    IconSource = new GlyphIconSource("\uE128") {FontSize = double.NaN},
                    ViewModelGenerator = () => new FontIconViewModel(),
                    TargetViewModelType = typeof (FontIconViewModel),
                },
            };
        }

        public ObservableCollection<ViewModelNavigationViewModel> Items { get; }

        public ICommand NavigateCommand { get; }
        public ICommand HistoryNavigateCommand { get; }

        private async void Navigate(ItemClickEventArgs e)
        {
            var item = (NavigationViewModelBase)e.ClickedItem;
            await item.NavigateAsync();
            //NavigationHelper.NavigateToExisting(item.ViewModelGenerator());
        }

        private void HistoryNavigate(ItemClickEventArgs e)
        {
            var item = (ViewModelBase)(IHistoryItemTarget)e.ClickedItem;
            NavigationHelper.NavigateToExisting(item);
        }

        public HistoryProvider History { get; } = HistoryProvider.Instance;
    }
}
