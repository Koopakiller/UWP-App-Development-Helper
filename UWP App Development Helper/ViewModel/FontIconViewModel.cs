using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Windows.Storage;
using Windows.Storage.Streams;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class FontIconViewModel : ViewModelBase
    {
        private string _searchTerm;
        private IReadOnlyCollection<FontIcon> _filteredMdl2FontIcons;
        private bool _searchInTags = true;
        private bool _searchInDescription = true;
        private bool _searchInEnumValue = true;

        public FontIconViewModel()
        {
            this.LoadFontIconsCommand = new RelayCommand(this.LoadFontIcons);
        }

        public ICommand LoadFontIconsCommand { get; }

        private async void LoadFontIcons()
        {
            await this.LoadFontIconsAsync();
        }

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

            this.UpdateFilteredView();
        }

        public string SearchTerm
        {
            get { return this._searchTerm; }
            set
            {
                this._searchTerm = value;
                this.RaisePropertyChanged();
                this.UpdateFilteredView();
            }
        }

        public bool SearchInTags
        {
            get { return this._searchInTags; }
            set
            {
                this._searchInTags = value;
                this.RaisePropertyChanged();
                this.UpdateFilteredView();
            }
        }

        public bool SearchInDescription
        {
            get { return this._searchInDescription; }
            set
            {
                this._searchInDescription = value;
                this.RaisePropertyChanged();
                this.UpdateFilteredView();
            }
        }

        public bool SearchInEnumValue
        {
            get { return this._searchInEnumValue; }
            set
            {
                this._searchInEnumValue = value;
                this.RaisePropertyChanged();
                this.UpdateFilteredView();
            }
        }

        public FontIconCollection Mdl2 { get; set; }

        public IReadOnlyCollection<FontIcon> FilteredMdl2FontIcons
        {
            get { return this._filteredMdl2FontIcons; }
            private set
            {
                this._filteredMdl2FontIcons = value;
                this.RaisePropertyChanged();
            }
        }

        private void UpdateFilteredView()
        {
            const int code = 1033;

            if (string.IsNullOrEmpty(this.SearchTerm))
            {
                this.FilteredMdl2FontIcons = this.Mdl2.FontIcons.ToList();
            }
            else
            {
                var parts = this.SearchTerm.Split(' ');
                this.FilteredMdl2FontIcons =
                    this.Mdl2.FontIcons.Select(x =>
                        Tuple.Create(x,
                            (this.SearchInTags ? x.Tags
                                                  .FirstOrDefault(y => y.LanguageCode == code)
                                                  .LanguageSpecific
                                                  .Sum(y => parts.Count(z => y.IndexOf(z, StringComparison.OrdinalIgnoreCase) != -1))
                                               : 0) +
                            (this.SearchInDescription ? parts.Count(z => (x.Descriptions.FirstOrDefault(y => y.LanguageCode == code)?.Text?.IndexOf(z, StringComparison.OrdinalIgnoreCase) ?? -1) != -1) : 0) +
                            (this.SearchInEnumValue ? parts.Count(z => (x.EnumValue?.IndexOf(z, StringComparison.OrdinalIgnoreCase) ?? -1) != -1) : 0)
                            )).Where(x => x.Item2 > 0).OrderByDescending(x => x.Item2).Select(x => x.Item1).ToList();
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