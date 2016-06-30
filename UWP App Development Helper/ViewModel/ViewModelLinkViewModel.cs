using System;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class ViewModelLinkViewModel: ViewModelBase
    {
        public Type ViewModelType { get; set; }

        public string Header { get; set; }

        public Func<ViewModelBase >ViewModelGenerator { get; set; }

        public string Glyph { get; set; }
    }
}
