using System.Collections.ObjectModel;
using System.Diagnostics;
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
            IAnalyticsService analyticsService)
            : base(navigationService, analyticsService)
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


        public override void Initialize(INavigationParameters parameters)
        {
            LoadTheData();
        }

        private async void LoadTheData()
        {
            IsRefreshing = true;
            Items.Clear();
            Items = new ObservableCollection<SpeakerModel>(await _speakerService.GetSpeakersAsync());
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
            var p = new NavigationParameters {{"Id", speaker.Id}};

            var result = await NavigationService.NavigateAsync("SpeakerDetailPage", p, false);

            if (!result.Success) Debug.WriteLine("Could not nav to SpeakerDetailPage");
        }


        private void OnNavigateToProfile()
        {
        }
    }
}