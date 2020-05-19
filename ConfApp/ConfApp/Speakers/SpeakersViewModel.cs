using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using ConfApp.Services;
using ConfApp.Services.Telemetry;
using ConfApp.ViewModels;
using Prism.Commands;
using Prism.Navigation;

namespace ConfApp.Speakers
{
    public class SpeakersViewModel : TabViewModelBase
    {
        private readonly ISpeakerService _speakerService;

        private bool _isRefreshing;
        private ObservableCollection<SpeakerModel> _items = new ObservableCollection<SpeakerModel>();

        public SpeakersViewModel(
            INavigationService navigationService,
            ISpeakerService speakerService,
            ITelemetryService telemetryService)
            : base(navigationService, telemetryService)
        {
            _speakerService = speakerService;
            Title = "Speakers";
            NavigateToProfileCommand = new DelegateCommand(LoadTheData);
            NavigateToSpeakerCommand = new DelegateCommand<SpeakerModel>(OnNavigateToSpeaker);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public DelegateCommand<SpeakerModel> NavigateToSpeakerCommand { get; set; }
        public DelegateCommand NavigateToProfileCommand { get; set; }

        public ObservableCollection<SpeakerModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }


        public override async void Initialize(INavigationParameters parameters)
        {
            await Task.Delay(2000);
            LoadTheData();
        }

        private async void LoadTheData()
        {
            IsRefreshing = true;
            Items.Clear();
            foreach (var item in await _speakerService.GetSpeakersAsync()) Items.Add(item);

            IsRefreshing = false;
        }

        public override void OnAppearing()
        {
        }

        public override void OnDisappearing()
        {
        }

        private async void OnNavigateToSpeaker(SpeakerModel speaker)
        {
            var result = await NavigationService.NavigateAsync("SpeakerDetailPage", null, false, false);

            if (!result.Success) Debug.WriteLine("Could not nav to SpeakerDetailPage");
        }


        private void OnNavigateToProfile()
        {
        }
    }
}