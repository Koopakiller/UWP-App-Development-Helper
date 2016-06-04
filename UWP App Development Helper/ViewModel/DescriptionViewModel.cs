using GalaSoft.MvvmLight;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class DescriptionViewModel : ViewModelBase
    {
        public DescriptionViewModel(int langCode, string text)
        {
            this.LanguageCode = langCode;
            this.Text = text;
        }
        public int LanguageCode { get; set; }
        public string Text { get; }
    }
}