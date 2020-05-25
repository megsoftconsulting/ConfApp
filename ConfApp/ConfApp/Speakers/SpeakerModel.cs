using System.Collections.ObjectModel;
using ConfApp.Talks;
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

        public SpeakerModel(string id, string firstName, string lastName, string description, string image,
            string bigImage, string company)
        {
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            Description = description;
            Image = image;
            BigImage = bigImage;
            SmallImage = Image;
            Id = id;
            Bio =
                @"Alice is a developer advocate at IBM, working with Open Liberty and Reactive Platform. She has now been with IBM for a year, after graduating from Exeter University with a Degree in Biology.

Moving to software engineering has been a challenging step for Grace, but she enjoys bringing a varied perspective to her projects and using her knowledge of biological systems to simplify complex software patterns and architectures. 

As a developer advocate, Grace builds POC’s, demos and sample applications, and writes guides and tutorials to help guide users through technologies and products. Grace also has a keen passion for encouraging more women into STEM and especially Technology careers.";
        }

        public ObservableCollection<TalkModel> Talks { get; set; }
        public string Bio { get; set; }
        public string SmallImage { get; set; }

        public string Id { get; set; }

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