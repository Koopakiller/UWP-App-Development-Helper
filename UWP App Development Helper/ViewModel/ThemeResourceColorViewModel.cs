using System;
using System.Collections.Generic;
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
            this.LoadThemeResourcesCommand = new RelayCommand(async () => await this.LoadThemeResourcesAsync());
        }

        private bool _isLoading;
        private IReadOnlyCollection<ColorViewModel> _themeResources ;

        public IReadOnlyCollection<ColorViewModel> ThemeResources
        {
            get { return this._themeResources; }
            set
            {
                this._themeResources = value;
                this.RaisePropertyChanged();
            }
        }

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
            if (this.ThemeResources!=null)
            {
                return;
            }

            this.IsLoading = true;
            
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Resources/ThemeResources.txt"));
            this.ThemeResources = (await FileIO.ReadLinesAsync(file, UnicodeEncoding.Utf8)).Select(line => new ColorViewModel(line)).ToList();

            this.IsLoading = false;
        }
    }

}