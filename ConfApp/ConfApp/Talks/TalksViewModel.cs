using System.Collections.ObjectModel;
using ConfApp.ViewModels;
using Prism.Commands;
using Prism.Navigation;

namespace ConfApp.Talks
{
    public class TalksViewModel : ViewModelBase
    {
        private ObservableCollection<TalkModel> _items = new ObservableCollection<TalkModel>();

        public TalksViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Sessions";
            SelectedItemCommand = new DelegateCommand<TalkModel>(OnSelectedItem);
        }

        private async void OnSelectedItem(TalkModel obj)
        {
            await NavigationService.NavigateAsync("TalksDetailPage");
        }

        public DelegateCommand<TalkModel> SelectedItemCommand { get; set; }
        public DelegateCommand NavigateToProfileCommand { get; set; }

        public ObservableCollection<TalkModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            AddMockData();
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