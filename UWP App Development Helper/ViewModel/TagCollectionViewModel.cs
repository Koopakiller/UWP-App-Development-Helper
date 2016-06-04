using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class TagCollectionViewModel:ViewModelBase
    {
        public TagCollectionViewModel(params string[] tags)
        {
            this.LanguageSpecific = new List<string>(tags);
        }
        public int LanguageCode { get; set; }
        public IList<string> LanguageSpecific { get; }
    }
}