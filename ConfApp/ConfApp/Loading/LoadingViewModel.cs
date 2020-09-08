using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConfApp.About;
using ConfApp.Services;
using ConfApp.Services.Telemetry;
using ConfApp.Services.Telemetry.Events;
using ConfApp.Speakers;
using ConfApp.Sync;
using ConfApp.Talks;
using ConfApp.ViewModels;
using Prism.Navigation;
using Prism.Services.Dialogs;

namespace ConfApp.Loading
{
    public class LoadingViewModel : ViewModelBase
    {
        private readonly IAnalyticsService _analytics;
        private readonly IDialogService _dialogService;
        private readonly IPromptService _promptService;
        private bool _loadingInProgress;
        private double _progress;
        private string _version;

        public LoadingViewModel(
            INavigationService navigationService,
            IAnalyticsService analytics, 
            IDialogService dialogService,
            IPromptService promptService
        ) : base(navigationService, analytics)
        {
            _analytics = analytics;
            _dialogService = dialogService;
            _promptService = promptService;
            Title = "Loading Page";
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
            var @event = new AppLoadedEvent();
            LoadingInProgress = true;
            await UpdateProgress();
            LoadingInProgress = false;
            @event.EventCompleted();
            
            var r = await NavigationService.NavigateAsync("/LoginPage");
            if (r.Success)
                _analytics.TrackEvent(@event);
            else
            {
                _analytics.TrackError(r.Exception, null);
                await _promptService.DisplayAlert("Error", r.Exception.InnerException?.Message, "OK");

            }
        }

        private async Task NavigateToTabbedPage()
        {
           await PageNavigationUtil.NavigateToMainPageAsync(NavigationService);
        }

        private async Task UpdateProgress()
        {
            for (var i = 0; i < 100; i++)
            {
                await Task.Delay(10);
                var result = (double) i / 100;
                Progress = result;
            }
        }
    }

    public static class PageNavigationUtil
    {
        public static Task NavigateToMainPageAsync(INavigationService service)
        {
            var sb = new StringBuilder("/MainPage?");
            sb.Append($"createTab={nameof(BigTitleNavigationPage)}|{nameof(SpeakersPage)}");
            sb.Append($"&createTab={nameof(BigTitleNavigationPage)}|{nameof(TalksPage)}");
            sb.Append($"&createTab={nameof(BigTitleNavigationPage)}|{nameof(AboutPage)}");
            sb.Append($"&createTab={nameof(BigTitleNavigationPage)}|{nameof(SyncPage)}");
            return service.NavigateAsync(sb.ToString());
        }
    }
}