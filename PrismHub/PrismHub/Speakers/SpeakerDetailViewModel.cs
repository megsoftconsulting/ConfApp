using ConfApp.ViewModels;
using Prism.Navigation;

namespace ConfApp.Speakers
{
    public class SpeakerDetailViewModel : ViewModelBase
    {
        public SpeakerDetailViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}