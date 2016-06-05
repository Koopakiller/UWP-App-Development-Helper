using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class SingleFontIconViewModel:ViewModelBase
    {
        public SingleFontIconViewModel(params char[] icons)
        {
            this.Chars = new List<char>(icons);
        }
        public SingleFontIconViewModel(char[] icons, IList<string> tags, string description) : this(icons)
        {
            this.Tags = tags;
            this.Description = description;
        }

        public IList<char> Chars { get; }

        public IList<string> Tags { get; } = new List<string>();
        public string Description { get; }

        public string EnumValue { get; set; }
    }
}