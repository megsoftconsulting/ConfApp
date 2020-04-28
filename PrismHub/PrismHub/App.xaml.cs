using System.Text;
using System.Threading.Tasks;
using ConfApp.About;
using ConfApp.Loading;
using ConfApp.Speakers;
using ConfApp.Talks;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
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
            await NavigateToTabbedPage();
            //await NavigationService.NavigateAsync("/LoadingPage");
        }

        private async Task NavigateToTabbedPage()
        {
            var sb = new StringBuilder("/MainTabbedPage?");
            sb.Append($"createTab={nameof(MyNavigationPage)}|{nameof(SpeakersPage)}");
            sb.Append($"&createTab={nameof(MyNavigationPage)}|{nameof(TalksPage)}");
            sb.Append($"&createTab={nameof(MyNavigationPage)}|{nameof(AboutPage)}");
            //sb.Append($"&createTab={nameof(MyNavigationPage)}|{nameof(TalksPage)}");
            await NavigationService.NavigateAsync(sb.ToString());
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MyNavigationPage>();
            containerRegistry.RegisterForNavigation<MainTabbedPage>();
            containerRegistry.RegisterForNavigation<SpeakersPage, SpeakersViewModel>();
            containerRegistry.RegisterForNavigation<TalksPage, TalksViewModel>();
            containerRegistry.RegisterForNavigation<TalkDetailPage, TalkDetailViewModel>();
            containerRegistry.RegisterForNavigation<LoadingPage, LoadingViewModel>();
            containerRegistry.RegisterForNavigation<SpeakerDetailPage, SpeakerDetailViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutViewModel>();


        }
    }
}