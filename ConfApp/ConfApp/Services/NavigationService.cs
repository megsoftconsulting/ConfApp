using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ConfApp.Services.Telemetry;
using Prism.Behaviors;
using Prism.Common;
using Prism.Ioc;
using Prism.Logging;
using Prism.Navigation;
using Xamarin.Forms;

namespace ConfApp.Services
{
    public class NavigationService : PageNavigationService
    {
        private readonly ITelemetryService _telemetryService;
        private string _mostRecentPageTitle = string.Empty;

        public NavigationService(IContainerExtension container,
            IApplicationProvider applicationProvider,
            IPageBehaviorFactory pageBehaviorFactory,
            ILoggerFacade logger,
            ITelemetryService telemetryService) : base(container, applicationProvider,
            pageBehaviorFactory, logger)
        {
            _telemetryService = telemetryService;
        }

        protected override Task<INavigationResult> NavigateInternal(Uri uri, INavigationParameters parameters,
            bool? useModalNavigation, bool animated)
        {
            var result = base.NavigateInternal(uri, parameters, useModalNavigation, animated);

            //if (result.Result.Success)
            //    _telemetryService.TrackEvent(new EventBase($"Navigated to {_mostRecentPageTitle?}"));

            return result;
        }

        protected override Task<Page> DoPop(INavigation navigation, bool useModalNavigation, bool animated)
        {
            Debug.WriteLine("Do Pop");
            return base.DoPop(navigation, useModalNavigation, animated);
        }

        public override Task<INavigationResult> GoBackAsync()
        {
            Debug.WriteLine("Go Back");
            return base.GoBackAsync();
        }

        protected override Task DoPush(Page currentPage, Page page, bool? useModalNavigation, bool animated,
            bool insertBeforeLast = false,
            int navigationOffset = 0)
        {
            _mostRecentPageTitle = string.IsNullOrWhiteSpace(page.Title) ? page.GetType().Name : page.Title;
            // Debug.WriteLine($"Navigating to {_mostRecentPageTitle}");

            return base.DoPush(currentPage, page, useModalNavigation, animated, insertBeforeLast, navigationOffset);
        }
    }
}