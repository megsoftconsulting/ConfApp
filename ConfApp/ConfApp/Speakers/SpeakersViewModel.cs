using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ConfApp.Services;
using ConfApp.ViewModels;
using DynamicData;
using DynamicData.PLinq;
using Prism.Commands;
using Prism.Navigation;

namespace ConfApp.Speakers
{
    public class SpeakersViewModel : ViewModelBase, IInitialize
    {
        private readonly ISpeakerService _speakerService;
        private ReadOnlyObservableCollection<SpeakerModel> _items;

        public SpeakersViewModel(INavigationService navigationService, ISpeakerService speakerService)
            : base(navigationService)
        {
            _speakerService = speakerService;
            Title = "Speakers";
            NavigateToProfileCommand = new DelegateCommand(OnNavigateToProfile);
            NavigateToSpeakerCommand = new DelegateCommand<SpeakerModel>(OnNavigateToSpeaker);
            IsActiveChanged += OnIsActiveChanged;

            _speakerService
                .ChangeSet
                .Bind(out _items)
                .DisposeMany()
                .Subscribe();
        }
       
        public DelegateCommand<SpeakerModel> NavigateToSpeakerCommand { get; set; }
        public DelegateCommand NavigateToProfileCommand { get; set; }

        public ReadOnlyObservableCollection<SpeakerModel> Items => _items;

        public async void Initialize(INavigationParameters parameters) => await _speakerService.Get();

        private async void OnNavigateToSpeaker(SpeakerModel speaker)
        {
            var result = await NavigationService.NavigateAsync("SpeakerDetailPage", null, false, false);

            if (!result.Success) Debug.WriteLine("Could not nav to SpeakerDetailPage");
        }

        private void OnIsActiveChanged(object sender, EventArgs e)
        {
            if (IsActive)
                Debug.WriteLine("Speakers Tab Is Active");
        }

        private void OnNavigateToProfile()
        {
        }

        public override void Destroy()
        {
            IsActiveChanged -= OnIsActiveChanged;
        }
    }
}