using ConfApp.Services.Telemetry;
using ConfApp.ViewModels;
using Prism.Navigation;

namespace ConfApp
{
    public class TabbedViewModel : ViewModelBase
    {
        public TabbedViewModel(
            INavigationService navigationService,
            ITelemetryService telemetryService) :
            base(navigationService, telemetryService)
        {
        }
    }
}