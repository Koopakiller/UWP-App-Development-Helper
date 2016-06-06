using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class ColorsViewModel : ViewModelBase
    {
        public ColorsViewModel()
        {
            this.SubSections = new ObservableCollection<HeaderViewModelBase>()
            {
                new AccentColorsViewModel()
                {
                    Header ="Accent Colors",
                },
                new ThemeResourceColorViewModel()
                {
                    Header="ThemeResource Colors",
                }
            };
        }

        public ObservableCollection<HeaderViewModelBase> SubSections { get; }
    }
}
