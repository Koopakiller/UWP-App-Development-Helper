using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Helper;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Model;
using PostSharp.Patterns.Model;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    [NotifyPropertyChanged]
    public class FontIconViewModel : ViewModelBase
    {
        #region Fields

        private IReadOnlyCollection<SingleFontIconViewModel> _filteredMdl2FontIcons;
        private string _searchTerm;
        private bool _isLoading = true;
        private CancellationTokenSource _lastFilterFontIconListCancellationTokenSource = new CancellationTokenSource();
        private bool _searchInTags = true;
        private bool _searchInDescription = true;
        private bool _searchInEnumValue = true;

        #endregion

        #region .ctor

        public FontIconViewModel()
        {
            this.NavigateToFontIconDetailsCommand = new RelayCommand<ItemClickEventArgs>(this.NavigateToFontIconDetails);
            this.LoadFontIconsCommand = new RelayCommand(async () => await this.LoadFontIconsAsync());
            this.FilterFontIconListCommand = new RelayCommand(async () => await this.FilterFontIconListAsync(new CancellationTokenSource()));
        }

        #endregion

        #region Properties

        public ICommand LoadFontIconsCommand { get; }

        public ICommand FilterFontIconListCommand { get; }

        public ICommand NavigateToFontIconDetailsCommand { get; }

        public string SearchTerm
        {
            get { return this._searchTerm; }
            set
            {
                this._searchTerm = value;
                this.RaisePropertyChanged();
                this.FilterFontIconListCommand.Execute(null);
            }
        }

        public bool SearchInTags
        {
            get { return this._searchInTags; }
            set
            {
                this._searchInTags = value;
                this.RaisePropertyChanged();
                this.FilterFontIconListCommand.Execute(null);
            }
        }

        public bool SearchInDescription
        {
            get { return this._searchInDescription; }
            set
            {
                this._searchInDescription = value;
                this.RaisePropertyChanged();
                this.FilterFontIconListCommand.Execute(null);
            }
        }

        public bool SearchInEnumValue
        {
            get { return this._searchInEnumValue; }
            set
            {
                this._searchInEnumValue = value;
                this.RaisePropertyChanged();
                this.FilterFontIconListCommand.Execute(null);
            }
        }

        public FontIconCollectionViewModel Mdl2 { get; set; }

        public bool IsLoading
        {
            get { return this._isLoading; }
            set
            {
                this._isLoading = value;
                this.RaisePropertyChanged();
            }
        }

        public IReadOnlyCollection<SingleFontIconViewModel> FilteredMdl2FontIcons
        {
            get { return this._filteredMdl2FontIcons; }
            private set
            {
                this._filteredMdl2FontIcons = value;
                this.RaisePropertyChanged();
            }
        }

        #endregion

        private async Task LoadFontIconsAsync()
        {
            const int code = 1033;
            var xmlFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Resources/mdl2.xml"));

            var xmlText = await FileIO.ReadTextAsync(xmlFile, UnicodeEncoding.Utf8);

            var doc = XDocument.Parse(xmlText);

            this.Mdl2 = new FontIconCollectionViewModel(
                doc.Root.Elements("FontIcon")
                    .Select(x => new SingleFontIconViewModel(
                        x.Elements("Code")
                         .Select(y => (char)Convert.ToInt32(y.Value, 16))
                         .ToArray(),
                        x.Elements("Tags")
                            ?.FirstOrDefault(y => int.Parse(y.Attribute("Language").Value) == code)
                            ?.Elements("Tag")
                            ?.Select(y => y.Value)
                            ?.ToList(),
                        x.Elements("Description")?
                            .FirstOrDefault(y => int.Parse(y.Attribute("Language").Value) == code)?
                            .Value)
                    {
                        EnumValue = x.Element("EnumValue")?.Value,
                    })
                    .ToList())
            {
                FontName = "Segoe MDL2 Assets",
            };

            await this.FilterFontIconListAsync(new CancellationTokenSource());
        }

        private async Task FilterFontIconListAsync(CancellationTokenSource cts)
        {
            this._lastFilterFontIconListCancellationTokenSource.Cancel();
            this._lastFilterFontIconListCancellationTokenSource = cts;

            await DispatcherHelper.RunAsync(() => this.IsLoading = true);
            await Task.Delay(250);

            IReadOnlyCollection<SingleFontIconViewModel> result;
            if (string.IsNullOrEmpty(this.SearchTerm))
            {
                result = this.Mdl2.FontIcons.ToList();
            }
            else
            {
                var parts = this.SearchTerm.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                result =
                    this.Mdl2.FontIcons
                             .Select(x => Tuple.Create(x,
                                                (this.SearchInTags ? x.Tags
                                                                      .Sum(y => parts.Count(z => y.IndexOf(z, StringComparison.OrdinalIgnoreCase) != -1))
                                                                   : 0) +
                                                (this.SearchInDescription ? parts.Count(z => (x.Description?.IndexOf(z, StringComparison.OrdinalIgnoreCase) ?? -1) != -1)
                                                                          : 0) +
                                                (this.SearchInEnumValue ? parts.Count(z => (x.EnumValue?.IndexOf(z, StringComparison.OrdinalIgnoreCase) ?? -1) != -1)
                                                                        : 0)))
                             .Where(x => x.Item2 > 0)
                             .OrderByDescending(x => x.Item2)
                             .Select(x => x.Item1)
                             .ToList();
            }

            if (!cts.IsCancellationRequested)
            {
                await DispatcherHelper.RunAsync(() => this.FilteredMdl2FontIcons = result);

                await DispatcherHelper.RunAsync(() => this.IsLoading = false);
            }
        }

        private void NavigateToFontIconDetails(ItemClickEventArgs e)
        {
            var fi = (SingleFontIconViewModel)e.ClickedItem;
            fi.Caller = this;
            NavigationHelper.NavigateToExisting(fi);
        }

    }
}