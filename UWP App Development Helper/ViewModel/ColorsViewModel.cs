using System.Collections.ObjectModel;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class ColorsViewModel : ViewModelBase 
    {
        private HeaderViewModelBase _selectedSubSection;

        public ColorsViewModel()
        {
            this.SubSections = new ObservableCollection<HeaderViewModelBase>()
            {
                new AccentColorsViewModel(),
                new ThemeResourceColorViewModel(),
            };
        }

        public ObservableCollection<HeaderViewModelBase> SubSections { get; }

        public HeaderViewModelBase SelectedSubSection
        {
            get { return this._selectedSubSection; }
            set
            {
                this._selectedSubSection = value;
                this.RaisePropertyChanged();
            }
        }
    }
}
