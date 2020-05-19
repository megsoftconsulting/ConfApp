using ConfApp.Services.Telemetry;
using ConfApp.ViewModels;
using Prism.Navigation;

namespace ConfApp.Talks
{
    public class TalkDetailViewModel : ViewModelBase
    {
        public TalkDetailViewModel(
            INavigationService navigationService,
            ITelemetryService telemetryService) :
            base(navigationService, telemetryService)
        {
        }
    }
}