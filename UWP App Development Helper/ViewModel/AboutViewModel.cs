using System;
using System.Reflection;
using Windows.ApplicationModel;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Extensions;
using ProcessorArchitecture = Windows.System.ProcessorArchitecture;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class AboutViewModel : ViewModelBase 
    {
        public AboutViewModel()
        {
            var asm = typeof(App).GetTypeInfo().Assembly;
            this.AssemblyVersion = asm.GetCustomAttribute<AssemblyVersionAttribute>()?.Version;
            this.AssemblyFileVersion = asm.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
            this.AssemblyCopyright = asm.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
            this.AssemblyProduct = asm.GetCustomAttribute<AssemblyProductAttribute>()?.Product;

            this.PackageVersion = Package.Current.Id.Version.AsVersion();
            this.PackageArchitecture = Package.Current.Id.Architecture;

        }

        public string AssemblyVersion { get; set; }
        public string AssemblyFileVersion { get; set; }
        public string AssemblyCopyright { get; set; }
        public string AssemblyProduct { get; set; }

        public Version PackageVersion { get; set; }
        public ProcessorArchitecture PackageArchitecture { get; set; }
    }

}