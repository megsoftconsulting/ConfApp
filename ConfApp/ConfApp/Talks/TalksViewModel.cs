using System.Collections.ObjectModel;
using System.Diagnostics;
using ConfApp.Services.Telemetry;
using ConfApp.ViewModels;
using Prism.Commands;
using Prism.Navigation;

namespace ConfApp.Talks
{
    public class TalksViewModel : TabViewModelBase
    {
        private ObservableCollection<TalkModel> _items = new ObservableCollection<TalkModel>();

        public TalksViewModel(INavigationService navigationService,
            ITelemetryService telemetryService)
            : base(navigationService, telemetryService)
        {
            Title = "Sessions";
            SelectedItemCommand = new DelegateCommand<TalkModel>(OnSelectedItem);
            SetFavoriteCommand = new DelegateCommand<TalkModel>(OnTalkFavorited);
            IsTabActiveChanged += OnIsTabActiveChanged;
        }

        public DelegateCommand<TalkModel> SelectedItemCommand { get; set; }
        public DelegateCommand NavigateToProfileCommand { get; set; }
        public DelegateCommand<TalkModel> SetFavoriteCommand { get; }

        public ObservableCollection<TalkModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private void OnIsTabActiveChanged(object sender, bool e)
        {
            _items.Clear();
            AddMockData();
        }

        private void OnTalkFavorited(TalkModel talk)
        {
        }

        private async void OnSelectedItem(TalkModel obj)
        {
            var r = await NavigationService.NavigateAsync("TalkDetailPage");
            if (r.Success) Debug.WriteLine("Success");
        }

        private void AddMockData()
        {
            AddTalk("Solving Diabetes with an Open Source Artificial Pancreas",
                "Grace Hansen", "IBM", "11:00AM - 12:30PM", "Breakout Room A", "Session", "Grace");
            AddTalk("Building Your First Voice Experience with Alexa", "Bessie Silva", "Apple", "2:00PM-3:45PM",
                "Beach", "Outdoors", "Mofi");
            AddTalk("Creating Smarter ChatBots with NLP & Node.js", "Alexander Sanchez", "Megsoft", "4:00PM - 5:00PM",
                "Keynote", "Keynote", "Rob");
            AddTalk("From Zero to DevOps Superhero: The Container Edition", "Pete George", "Oracle", "8AM - 2PM",
                "Downstairs", "Session", "Grace");
            AddTalk("Building Your First Voice Experience with Alexa", "Bessie Silva", "Apple", "2:00PM-3:45PM",
                "Beach", "Outdoors", "Mofi");
        }

        private void AddTalk(string title, string name, string employer, string when, string where, string st,
            string image)
        {
            _items.Add(
                new TalkModel
                {
                    Title = title,
                    SpeakerName = name,
                    Employer = employer,
                    When = when,
                    Where = where,
                    SessionType = st,
                    AvatarImage = image
                });
        }
    }
}