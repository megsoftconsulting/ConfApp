using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ConfApp.ViewModels;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace ConfApp.Speakers
{
    public class SpeakersViewModel : ViewModelBase
    {
        private ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();

        public SpeakersViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Speakers";
            NavigateToProfileCommand = new DelegateCommand(OnNavigateToProfile);
            AddMockData();
        }

        public DelegateCommand<ItemViewModel> SelectedItemCommand { get; set; }
        public DelegateCommand NavigateToProfileCommand { get; set; }

        public ObservableCollection<ItemViewModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public ItemViewModel SelectedItem { get; set; }

        private async void AddMockData()
        {
            await Task.Delay(500);
            await Device.InvokeOnMainThreadAsync(() =>
            {
                AddItem("Grace", "Jansen", "Developer Advocate", "Grace", "IBM");
                AddItem("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
                AddItem("Ned", "Stark", "The Hand of the King", "Scott", "King's Landing");
                AddItem("Jon", "Snow", "The true king", "Motica", "Winterfell");
                AddItem("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
                AddItem("Ned", "Stark", "The Hand of the King", "Scott", "King's Landing");
                AddItem("Jon", "Snow", "The true king", "Mofi", "Winterfell");
                AddItem("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
                AddItem("Ned", "Stark", "The Hand of the King", "Scott", "King's Landing");
                AddItem("Jon", "Snow", "The true king", "Jessica", "Winterfell");
                AddItem("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
                AddItem("Ned", "Stark", "The Hand of the King", "Mofi", "King's Landing");
                AddItem("Jon", "Snow", "The true king", "Jessica", "Winterfell");
                AddItem("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
                AddItem("Ned", "Stark", "The Hand of the King", "Scott", "King's Landing");
                AddItem("Jon", "Snow", "The true king", "Motica", "Winterfell");
                AddItem("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
                AddItem("Ned", " Stark", "The Hand of the King", "Scott", "King's Landing");
                AddItem("Jon", " Snow", "The true king", "Grace", "Winterfell");
                AddItem("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
                AddItem("Ned", "Stark", "The Hand of the King", "Mofi", "King's Landing");
            });
        }

        private void OnNavigateToProfile()
        {
        }

        private void AddItem(string firstName, string lastName, string description, string image, string company)
        {
            _items
                .Add(new ItemViewModel(NavigationService)
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Description = description,
                    Image = image,
                    Company = company
                });
        }
    }
}