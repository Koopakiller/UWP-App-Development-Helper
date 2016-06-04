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
        public SingleFontIconViewModel(char[] icons, IList<TagCollectionViewModel> tags, IList<DescriptionViewModel> descriptions) : this(icons)
        {
            this.Tags = tags;
            this.Descriptions = descriptions;
        }

        public IList<char> Chars { get; }

        public IList<TagCollectionViewModel> Tags { get; } = new List<TagCollectionViewModel>();
        public IList<DescriptionViewModel> Descriptions { get; } = new List<DescriptionViewModel>();

        public string EnumValue { get; set; }
    }
}