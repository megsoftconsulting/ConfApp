using System.Threading;
using System.Threading.Tasks;
using ConfApp.Services;
using ConfApp.ViewModels;
using Prism;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;

namespace ConfApp.About
{
    public class AboutViewModel : ViewModelBase, IActiveAware
    {
        private readonly IEventHubProducer _client;
        private readonly ILocationService _locationService;
        private readonly CancellationTokenSource _token = new CancellationTokenSource();
        private bool _isActive;

        public AboutViewModel(INavigationService navigationService,
            IEventHubProducer client,
            ILocationService locationService) : base(navigationService)
        {
            _client = client;
            _locationService = locationService;
            Title = "About";
            StartUpdatesCommand = new DelegateCommand(StartUpdates);
        }

        public DelegateCommand StartUpdatesCommand { get; set; }

        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        private async void StartUpdates()
        {
            var status = await _locationService.RequestPermissions();

            if (status == PermissionStatus.Granted)
                await Task
                    .Run(UpdateMyLocationPassively)
                    .ConfigureAwait(false);
        }

        private async Task UpdateMyLocationPassively()
        {
            while (true)
            {
                var l = await _locationService.GetCurrentLocationAsync();

                if (l == null) continue;
                _client.SendAsync(new HeartBeatMessage("Claudio's iPhone", l));
                await Task.Delay(3000);
            }
        }
    }
}