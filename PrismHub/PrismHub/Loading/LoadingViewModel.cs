using System.Text;
using System.Threading.Tasks;
using ConfApp.ViewModels;
using Prism.Navigation;

namespace ConfApp.Loading
{
    public class LoadingViewModel : ViewModelBase
    {
        private bool _loadingInProgress;
        private double _progress;
        private string _version;

        public LoadingViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public bool LoadingInProgress
        {
            get => _loadingInProgress;
            set => SetProperty(ref _loadingInProgress, value);
        }

        public double Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        public string Version
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadingInProgress = true;
            await UpdateProgress();
            LoadingInProgress = false;
            //var uri = new Uri("/MainTabbedPage/SpeakersPage", UriKind.Absolute);
            //await NavigationService.NavigateAsync(uri);
            await NavigateToTabbedPage();
        }

        private async Task NavigateToTabbedPage()
        {
            const string navigationPage = "MyNavigationPage";
            const string tabbedPage = "MainTabbedPage";
            
            var sb = new StringBuilder($"/{tabbedPage}?");

            sb.Append($"createTab={navigationPage}|TalksPage");
           // sb.Append($"&createTab={navigationPage}|TalksPage");
            sb.Append($"&createTab={navigationPage}|AboutPage");
            await NavigationService.NavigateAsync(sb.ToString());
        }

        private async Task UpdateProgress()
        {
            for (var i = 0; i < 100; i++)
            {
                await Task.Delay(5);
                var result = (double) i / 100;
                Progress = result;
            }
        }
    }
}