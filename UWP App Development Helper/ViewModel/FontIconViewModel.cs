using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class FontIconViewModel : ViewModelBase
    {
        private string _searchTerm;
        private IReadOnlyCollection<FontIcon> _filteredMdl2FontIcons;

        public FontIconViewModel()
        {
            this.Mdl2 = Model.Mdl2.GetFontIcons();
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
                this.FilteredMdl2FontIcons =
                    this.Mdl2.FontIcons.Where(x =>
                            x.Tags.Any(y =>
                                y.LanguageSpecific.First(z =>
                                    z.LanguageCode == code).Text.Contains(this.SearchTerm)))
                        .ToList();
            }
        }

    }

    public class FontIconCollection
    {
        public string FontName { get; set; }

        public IList<FontIcon> FontIcons { get; } = new List<FontIcon>();
    }

    public class FontIcon
    {
        public FontIcon(params char[] icons)
        {
            this.Chars = new List<char>(icons);
        }

        public IList<char> Chars { get; }

        public IList<Tag> Tags { get; } = new List<Tag>();
    }
    public class Tag
    {
        public IList<LanguageSpecificTag> LanguageSpecific { get; } = new List<LanguageSpecificTag>();
    }

    public class LanguageSpecificTag
    {
        public int LanguageCode { get; set; }
        public string Text { get; set; }
    }
}