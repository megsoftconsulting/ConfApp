using Prism.Mvvm;

namespace ConfApp.Speakers
{
    public class SpeakerModel : BindableBase
    {
        private string _bigImage;
        private string _company;
        private string _description;
        private string _firstname;
        private string _image;
        private string _lastName;

        public SpeakerModel()
        {
        }

        public SpeakerModel(string firstName, string lastName, string description, string image, string company)
        {
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            Description = description;
            Image = image;
            BigImage = "Big" + Image;
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

        public string BigImage
        {
            get => _bigImage;
            set => SetProperty(ref _bigImage, value);
        }
    }
}