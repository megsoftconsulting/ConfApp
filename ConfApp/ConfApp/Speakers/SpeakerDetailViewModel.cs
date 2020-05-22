using System;
using ConfApp.Services;
using ConfApp.Services.Telemetry;
using ConfApp.ViewModels;
using Prism.Commands;
using Prism.Navigation;

namespace ConfApp.Speakers
{
    public class SpeakerDetailViewModel : ViewModelBase
    {
        private readonly ISpeakerService _speakerService;
        private SpeakerModel _speakerModel;

        public SpeakerDetailViewModel(INavigationService navigationService,
            ISpeakerService speakerService,
            ITelemetryService telemetryService) : base(
            navigationService, telemetryService)
        {
            _speakerService = speakerService;
            CloseCommand = new DelegateCommand(async ()=> await NavigationService.GoBackAsync());
        }

        public DelegateCommand CloseCommand { get; set; }

        public SpeakerModel Speaker
        {
            get => _speakerModel;
            set => SetProperty(ref _speakerModel, value);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var id = parameters["Id"];
            Speaker = await _speakerService.GetSpeakerByIdAsync(id.ToString());
        }
    }
}