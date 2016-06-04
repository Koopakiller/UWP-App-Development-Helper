using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Windows.Storage;
using Windows.Storage.Streams;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using PostSharp.Patterns.Model;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    [NotifyPropertyChanged]
    public class FontIconViewModel : ViewModelBase
    {
        #region Fields

        private IReadOnlyCollection<FontIcon> _filteredMdl2FontIcons;
        private string _searchTerm;
        private bool _isLoading = true;
        private CancellationTokenSource _lastFilterFontIconListCancellationTokenSource = new CancellationTokenSource();

        #endregion

        #region .ctor

        public FontIconViewModel()
        {
            this.LoadFontIconsCommand = new RelayCommand(async () => await this.LoadFontIconsAsync());
            this.FilterFontIconListCommand = new RelayCommand(async () => await this.FilterFontIconListAsync(new CancellationTokenSource()));

            this.PropertyChanged += this.OnPropertyChanged;
        }

        #endregion

        #region Properties

        public ICommand LoadFontIconsCommand { get; }

        public ICommand FilterFontIconListCommand { get; }


        private async Task LoadFontIconsAsync()
        {
            var xmlFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Resources/mdl2.xml"));

            var xmlText = await FileIO.ReadTextAsync(xmlFile, UnicodeEncoding.Utf8);

            var doc = XDocument.Parse(xmlText);

            this.Mdl2 = new FontIconCollection(
                doc.Root.Elements("FontIcon")
                    .Select(x => new FontIcon(x.Elements("Code")
                        .Select(y => (char)Convert.ToInt32(y.Value, 16))
                        .ToArray(),
                        x.Elements("Tags")
                            .Select(y => new TagCollection(y.Elements("Tag")
                                .Select(z => z.Value)
                                .ToArray())
                            {
                                LanguageCode = int.Parse(y.Attribute("Language").Value)
                            })
                            .ToList(),
                        x.Elements("Description")
                            .Select(y => new Description(int.Parse(y.Attribute("Language").Value), y.Value))
                            .ToList())
                    {
                        EnumValue = x.Element("EnumValue")?.Value,
                    })
                    .ToList())
            {
                FontName = "Segoe MDL2 Assets",
            };

            await this.FilterFontIconListAsync(new CancellationTokenSource());
        }

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

        public bool SearchInTags { get; set; } = true;

        public bool SearchInDescription { get; set; } = true;

        public bool SearchInEnumValue { get; set; } = true;

        public FontIconCollection Mdl2 { get; set; }

        public bool IsLoading
        {
            get { return this._isLoading; }
            set
            {
                this._isLoading = value;
                this.RaisePropertyChanged();
            }
        }

        public IReadOnlyCollection<FontIcon> FilteredMdl2FontIcons
        {
            get { return this._filteredMdl2FontIcons; }
            private set
            {
                this._filteredMdl2FontIcons = value;
                this.RaisePropertyChanged();
            }
        }

        #endregion

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case nameof(this.SearchInEnumValue):
                case nameof(this.SearchInDescription):
                case nameof(this.SearchInTags):
                    this.FilterFontIconListCommand.Execute(null);
                    break;
            }
        }

        private async Task FilterFontIconListAsync(CancellationTokenSource cts)
        {
            this._lastFilterFontIconListCancellationTokenSource.Cancel();
            this._lastFilterFontIconListCancellationTokenSource = cts;

            await DispatcherHelper.RunAsync(() => this.IsLoading = true);
            await Task.Delay(250);
            const int code = 1033;

            IReadOnlyCollection<FontIcon> result;
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
                                                                      .FirstOrDefault(y => y.LanguageCode == code)
                                                                      .LanguageSpecific
                                                                      .Sum(y => parts.Count(z => y.IndexOf(z, StringComparison.OrdinalIgnoreCase) != -1))
                                                                   : 0) +
                                                (this.SearchInDescription ? parts.Count(z => (x.Descriptions.FirstOrDefault(y => y.LanguageCode == code)?.Text?.IndexOf(z, StringComparison.OrdinalIgnoreCase) ?? -1) != -1)
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

    }

    public class FontIconCollection
    {
        public FontIconCollection(IList<FontIcon> fontIcons)
        {
            this.FontIcons = fontIcons;
        }

        public string FontName { get; set; }

        public IList<FontIcon> FontIcons { get; }
    }

    public class FontIcon
    {
        public FontIcon(params char[] icons)
        {
            this.Chars = new List<char>(icons);
        }
        public FontIcon(char[] icons, IList<TagCollection> tags, IList<Description> descriptions) : this(icons)
        {
            this.Tags = tags;
            this.Descriptions = descriptions;
        }

        public IList<char> Chars { get; }

        public IList<TagCollection> Tags { get; } = new List<TagCollection>();
        public IList<Description> Descriptions { get; } = new List<Description>();

        public string EnumValue { get; set; }
    }
    public class TagCollection
    {
        public TagCollection(params string[] tags)
        {
            this.LanguageSpecific = new List<string>(tags);
        }
        public int LanguageCode { get; set; }
        public IList<string> LanguageSpecific { get; }
    }
    public class Description
    {
        public Description(int langCode, string text)
        {
            this.LanguageCode = langCode;
            this.Text = text;
        }
        public int LanguageCode { get; set; }
        public string Text { get; }
    }
}