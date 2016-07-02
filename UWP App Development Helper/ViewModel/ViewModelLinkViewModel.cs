using System;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Controls;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class ViewModelLinkViewModel : ViewModelBase
    {
        public Type ViewModelType { get; set; }

        public string Header { get; set; }

        public Func<ViewModelBase> ViewModelGenerator { get; set; }

        public IconSource IconSource { get; set; }
    }
}
