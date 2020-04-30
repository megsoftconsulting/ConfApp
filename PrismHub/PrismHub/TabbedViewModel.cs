using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ConfApp.Services;
using ConfApp.ViewModels;
using Prism.Navigation;
using Xamarin.Essentials;

namespace ConfApp
{
    public class TabbedViewModel : ViewModelBase
    {
        public TabbedViewModel(
            INavigationService navigationService) :
            base(navigationService)
        {
        }
    }
}