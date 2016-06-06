using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Streams;
using GalaSoft.MvvmLight.Command;
using PostSharp.Patterns.Model;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    [NotifyPropertyChanged]
    public class ThemeResourceColorViewModel : HeaderViewModelBase
    {
        public ThemeResourceColorViewModel()
        {
            this.LoadThemeResourcesCommand = new RelayCommand(async () => await this.LoadThemeResourcesAsync());
        }

        private bool _areThemeResourcesLoaded;
        private bool _isLoading;

        public ObservableCollection<ColorViewModel> ThemeResources { get; } = new ObservableCollection<ColorViewModel>();

        public ICommand LoadThemeResourcesCommand { get; }

        public bool IsLoading
        {
            get { return this._isLoading; }
            set
            {
                this._isLoading = value;
                this.RaisePropertyChanged();
            }
        }

        private async Task LoadThemeResourcesAsync()
        {
            if (this._areThemeResourcesLoaded)
            {
                return;
            }

            this.IsLoading = true;

            this.ThemeResources.Clear();
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Resources/ThemeResources.txt"));
            var lines = await FileIO.ReadLinesAsync(file, UnicodeEncoding.Utf8);
            foreach (var line in lines)
            {
                this.ThemeResources.Add(new ColorViewModel(line));
            }
            this._areThemeResourcesLoaded = true;

            this.IsLoading = false;
        }
    }

}