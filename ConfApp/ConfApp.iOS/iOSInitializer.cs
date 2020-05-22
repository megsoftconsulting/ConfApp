using System.Diagnostics.CodeAnalysis;
using ConfApp.iOS.Services;
using ConfApp.Services;
using IdentityModel.OidcClient.Browser;
using Prism;
using Prism.Ioc;

namespace ConfApp.iOS
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry c)
        {
            c.Register<IBrowser, AsWebAuthenticationSessionBrowser>();
            c.RegisterSingleton<IGeofencingService, GeofencingService>();
            c.RegisterSingleton<IScreenshotService, ScreenshotService>();
        }
    }
}