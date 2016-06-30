using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            this.Header = "ThemeResource Colors";
            this.LoadThemeResourcesCommand = new RelayCommand(async () => await this.LoadThemeResourcesAsync());
        }

        private bool _areThemeResourcesLoaded;
        private bool _isLoading = true;

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

            await Task.Delay(200); // "If you can't make it good, at least make it look good." that's to show the progressbar in every case

            this.ThemeResources.Clear();
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Resources/ThemeResources.txt"));
            var cvms = (await FileIO.ReadLinesAsync(file, UnicodeEncoding.Utf8)).Select(line => new ColorViewModel(line));
            foreach (var cvm in cvms)
            {
                //await Task.Delay(1);
                this.ThemeResources.Add(cvm);
            }
            this._areThemeResourcesLoaded = true;

            this.IsLoading = false;
        }
    }

}