using System.Collections.Generic;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class FontIconCollectionViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        public FontIconCollectionViewModel(IList<SingleFontIconViewModel> fontIcons)
        {
            this.FontIcons = fontIcons;
        }

        public string FontName { get; set; }

        public IList<SingleFontIconViewModel> FontIcons { get; }
    }
}