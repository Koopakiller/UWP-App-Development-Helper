using System;
using Windows.ApplicationModel;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.Extensions
{
    public static class PackageExtensions
    {
        public static Version AsVersion(this PackageVersion version)
        {
            return new Version(version.Major, version.Minor, version.Build, version.Revision);
        }
    }
}
