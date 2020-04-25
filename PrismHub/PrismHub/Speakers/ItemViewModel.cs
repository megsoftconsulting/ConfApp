using ConfApp.ViewModels;
using Prism.Commands;
using Prism.Navigation;

namespace ConfApp.Speakers
{
    public class ItemViewModel : ViewModelBase
    {
        private string _company;
        private string _description;
        private string _firstname;
        private string _image;

        private string _lastName;
        private string _name;

        public ItemViewModel(INavigationService navigationService) : base(navigationService)
        {
            ItemSelectedCommand = new DelegateCommand(OnItemSelected);
        }

        public string Company
        {
            get => _company;
            set => SetProperty(ref _company, value);
        }

        public string Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public DelegateCommand ItemSelectedCommand { get; set; }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                RaisePropertyChanged(nameof(Name));
                SetProperty(ref _lastName, value);
            }
        }

        public string FirstName
        {
            get => _firstname;
            set
            {
                SetProperty(ref _firstname, value);
                RaisePropertyChanged(nameof(Name));
            }
        }

        public string Name => _firstname + " " + _lastName;

        private async void OnItemSelected()
        {
            var p = new NavigationParameters
            {
                {nameof(Name), Name},
                {nameof(Description), Description}
            };
           await NavigationService.NavigateAsync("SpeakerDetailPage", p, false, false);
        }
    }
}