using GalaSoft.MvvmLight;
using PostSharp.Patterns.Model;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    [NotifyPropertyChanged]
    public class HeaderViewModelBase : ViewModelBase
    {
        public string Header { get; set; }
    }
}