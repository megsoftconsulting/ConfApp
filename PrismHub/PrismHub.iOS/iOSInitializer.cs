using System.Diagnostics.CodeAnalysis;
using Prism;
using Prism.Ioc;

namespace ConfApp.iOS
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}