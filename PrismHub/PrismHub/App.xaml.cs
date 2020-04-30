using System.Text;
using System.Threading.Tasks;
using ConfApp.About;
using ConfApp.Loading;
using ConfApp.Services;
using ConfApp.Speakers;
using ConfApp.Talks;
using Prism;
using Prism.Ioc;
using Prism.Modularity;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]


namespace ConfApp
{
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

        protected override async void OnInitialized()
        {
            InitializeComponent();
            // await NavigateToTabbedPage();
            await NavigationService.NavigateAsync("/LoadingPage");
        }

        private async Task NavigateToTabbedPage()
        {
            var sb = new StringBuilder("/MainTabbedPage?");
            sb.Append($"createTab={nameof(MyNavigationPage)}|{nameof(TalksPage)}");
            sb.Append($"&createTab={nameof(MyNavigationPage)}|{nameof(SpeakersPage)}");
            sb.Append($"&createTab={nameof(MyNavigationPage)}|{nameof(AboutPage)}");
            //sb.Append($"&createTab={nameof(MyNavigationPage)}|{nameof(TalksPage)}");
            await NavigationService.NavigateAsync(sb.ToString());
        }


        protected override void RegisterTypes(IContainerRegistry c)
        {
            RegisterPages(c);
            c.RegisterSingleton<ILocationService, LocationService>();
            c.RegisterSingleton<IEventHubProducer, PassiveLocationService>();
            c.RegisterSingleton<ISpeakerService, SpeakerService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog catalog)
        {
            catalog.AddModule<PaidFeaturesModule>(InitializationMode.OnDemand);
        }


        private static void RegisterPages(IContainerRegistry containerRegistry)
        {
            // TODO: Write some Reflection magic to Register A FooPage and FooViewModel together. 
            // Great piece for an article and/or Extension to make Open Source.
            containerRegistry.RegisterForNavigation<MyNavigationPage>();
            containerRegistry.RegisterForNavigation<MainTabbedPage, TabbedViewModel>();
            containerRegistry.RegisterForNavigation<SpeakersPage, SpeakersViewModel>();
            containerRegistry.RegisterForNavigation<TalksPage, TalksViewModel>();
            containerRegistry.RegisterForNavigation<TalkDetailPage, TalkDetailViewModel>();
            containerRegistry.RegisterForNavigation<LoadingPage, LoadingViewModel>();
            containerRegistry.RegisterForNavigation<SpeakerDetailPage, SpeakerDetailViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutViewModel>();
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