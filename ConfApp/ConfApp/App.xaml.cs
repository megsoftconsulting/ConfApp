using System;
using System.Text;
using System.Threading.Tasks;
using ConfApp.About;
using ConfApp.Loading;
using ConfApp.Login;
using ConfApp.Services;
using ConfApp.Services.Telemetry;
using ConfApp.Speakers;
using ConfApp.Talks;
using IdentityModel.OidcClient.Browser;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]


namespace ConfApp
{
    [AutoRegisterForNavigation]
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override void OnStart()
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            //await NavigateToTabbedPage();
            var r = await NavigationService.NavigateAsync("/LoadingPage");
            ////if (!r.Success) 
            ////    await App
            ////        .Current?.
            ////        MainPage?.
            ////        DisplayAlert("Could not Display Page",r.Exception.Message, "OK");
            if (!r.Success) throw r.Exception;
            AppCenterLog.Info("Navigation", "Navigated to LoadingPage");
        }

        
        private async Task NavigateToTabbedPage()
        {
            var sb = new StringBuilder("/MainPage?");
            sb.Append($"createTab={nameof(BigTitleNavigationPage)}|{nameof(TalksPage)}");
            sb.Append($"&createTab={nameof(BigTitleNavigationPage)}|{nameof(SpeakersPage)}");
            sb.Append($"&createTab={nameof(BigTitleNavigationPage)}|{nameof(AboutPage)}");
            //sb.Append($"&createTab={nameof(BigTitleNavigationPage)}|{nameof(TalksPage)}");
            await NavigationService.NavigateAsync(sb.ToString());
            AppCenterLog.Verbose("Navigation", "Navigated to ");
        }

        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterRequiredTypes(containerRegistry);

            containerRegistry.Register<INavigationService, NavigationService>(NavigationServiceName);
        }

        protected override void RegisterTypes(IContainerRegistry c)
        {
            RegisterPages(c);
            c.RegisterSingleton<ILocationService, LocationService>();
            c.RegisterSingleton<IEventHubProducer, PassiveLocationService>();
            c.RegisterSingleton<ISpeakerService, SpeakerService>();
            c.RegisterSingleton<ITelemetryService, TelemetryService>();
            c.Register<IPromptService, PromptService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog catalog)
        {
            catalog.AddModule<PaidFeaturesModule>(InitializationMode.OnDemand);
        }


        private static void RegisterPages(IContainerRegistry containerRegistry)
        {
            // TODO: Write some Reflection magic to Register A FooPage and FooViewModel together. 
            // Great piece for an article and/or Extension to make Open Source.
            containerRegistry.RegisterForNavigation<BigTitleNavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, TabbedViewModel>();
            containerRegistry.RegisterForNavigation<SpeakersPage, SpeakersViewModel>();
            containerRegistry.RegisterForNavigation<TalksPage, TalksViewModel>();
            containerRegistry.RegisterForNavigation<TalkDetailPage, TalkDetailViewModel>();
            containerRegistry.RegisterForNavigation<LoadingPage, LoadingViewModel>();
            containerRegistry.RegisterForNavigation<SpeakerDetailPage, SpeakerDetailViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
        }
    }

    public class PaidFeaturesModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Make some Registrations of classes, pages, viewmodels, etc.
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // If you need to initialize anything that is module specific, here is where you can go to town. 
            //.. and remember, I don't always initialize a module, but when I do, I do it fast.
        }
    }
}