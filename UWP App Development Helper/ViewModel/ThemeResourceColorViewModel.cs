using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Streams;
using GalaSoft.MvvmLight.Command;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class ThemeResourceColorViewModel : HeaderViewModelBase
    {
        public ThemeResourceColorViewModel()
        {
            this.LoadThemeResourcesCommand = new RelayCommand(async () => await this.LoadThemeResourcesAsync());
        }

        public ObservableCollection<ColorViewModel> ThemeResources { get; } = new ObservableCollection<ColorViewModel>();

        public ICommand LoadThemeResourcesCommand { get; }

        private async Task LoadThemeResourcesAsync()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Resources/ThemeResources.txt"));
            var lines = await FileIO.ReadLinesAsync(file, UnicodeEncoding.Utf8);
            foreach (var line in lines)
            {
                this.ThemeResources.Add(new ColorViewModel(line));
            }
        }
    }

}