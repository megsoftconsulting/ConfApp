using System.Collections.ObjectModel;
using System.Diagnostics;
using ConfApp.Services;
using ConfApp.ViewModels;
using Prism.Commands;
using Prism.Navigation;

namespace ConfApp.Speakers
{
    public class SpeakersViewModel : ViewModelBase
    {
        private readonly ISpeakerService _speakerService;
        private ObservableCollection<SpeakerModel> _items = new ObservableCollection<SpeakerModel>();
        private bool _isActiveScreen;

        public SpeakersViewModel(INavigationService navigationService, ISpeakerService speakerService)
            : base(navigationService)
        {
            _speakerService = speakerService;
            Title = "Speakers";
            NavigateToProfileCommand = new DelegateCommand(OnNavigateToProfile);
            NavigateToSpeakerCommand = new DelegateCommand<SpeakerModel>(OnNavigateToSpeaker);
            IsTabActiveChanged += OnTabActiveChanged;
        }

        public DelegateCommand<SpeakerModel> NavigateToSpeakerCommand { get; set; }
        public DelegateCommand NavigateToProfileCommand { get; set; }

        public ObservableCollection<SpeakerModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private void OnTabActiveChanged(object sender, bool e)
        {
            if (!_isActiveScreen) return;
            var status = e ? "Active" : "Inactive";
            Debug.WriteLine($"Speakers Tab Is {status}");
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            foreach (var item in await _speakerService.GetSpeakersAsync()) Items.Add(item);
        }

        public override void OnAppearing()
        {
            _isActiveScreen = true;
        }

        public override void OnDisappearing()
        {
            _isActiveScreen = false;
        }

        private async void OnNavigateToSpeaker(SpeakerModel speaker)
        {
            var result = await NavigationService.NavigateAsync("SpeakerDetailPage", null, false, false);

            if (!result.Success) Debug.WriteLine("Could not nav to SpeakerDetailPage");
        }


        private void OnNavigateToProfile()
        {
        }

        public override void Destroy()
        {
            IsTabActiveChanged -= OnTabActiveChanged;
        }
    }
}