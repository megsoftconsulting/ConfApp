using System;
using ConfApp.Services;
using ConfApp.ViewModels;
using Prism.Navigation;

namespace ConfApp.Speakers
{
    public class SpeakerDetailViewModel : ViewModelBase
    {
        private readonly ISpeakerService _speakerService;
        private SpeakerModel _speakerModel;

        public SpeakerDetailViewModel(INavigationService navigationService, ISpeakerService speakerService) : base(
            navigationService)
        {
            _speakerService = speakerService;
        }

        public SpeakerModel Speaker
        {
            get => _speakerModel;
            set => SetProperty(ref _speakerModel, value);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Speaker = await _speakerService.GetSpeakerByIdAsync(Guid.NewGuid().ToString());
        }
    }
}