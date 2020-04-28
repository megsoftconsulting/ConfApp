using ConfApp.ViewModels;
using Prism.Navigation;

namespace ConfApp.About
{
    public class AboutViewModel : ViewModelBase
    {
        public AboutViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "About";
        }
    }
}