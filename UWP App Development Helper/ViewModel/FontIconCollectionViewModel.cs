using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class FontIconCollectionViewModel: GalaSoft.MvvmLight.ViewModelBase
    {
        public FontIconCollectionViewModel(IList<SingleFontIconViewModel> fontIcons)
        {
            this.FontIcons = fontIcons;
        }

        public string FontName { get; set; }

        public IList<SingleFontIconViewModel> FontIcons { get; }
    }
}