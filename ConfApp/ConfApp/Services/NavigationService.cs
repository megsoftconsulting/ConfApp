using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ConfApp.Services.Telemetry;
using ConfApp.Services.Telemetry.Events;
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
        private readonly IAnalyticsService _analyticsService;
        private string _mostRecentPageTitle = string.Empty;

        public NavigationService(IContainerExtension container,
            IApplicationProvider applicationProvider,
            IPageBehaviorFactory pageBehaviorFactory,
            ILoggerFacade logger,
            IAnalyticsService analyticsService) : base(container, applicationProvider,
            pageBehaviorFactory, logger)
        {
            _analyticsService = analyticsService;
        }

        protected override async Task<INavigationResult> NavigateInternal(Uri uri, INavigationParameters parameters,
            bool? useModalNavigation, bool animated)
        {
            var result = await base.NavigateInternal(uri, parameters, useModalNavigation, animated);

            if (result.Success)
            {
                _analyticsService.TrackEvent(new EventBase($"Navigated to {_mostRecentPageTitle}"));
                Debug.WriteLine($"Navigated to {uri.OriginalString} - {_mostRecentPageTitle}");
            }
            else
            {
                _analyticsService.TrackError(result.Exception);
                Debug.WriteLine($"Error while trying to navigate to {_mostRecentPageTitle}");
            }

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

        protected override Task ProcessNavigationForRootPage(string nextSegment, Queue<string> segments, INavigationParameters parameters,
            bool? useModalNavigation, bool animated)
        {
            Debug.WriteLine($"ProcessNavigationForRootPage - {nextSegment} - {nextSegment}");
            return base.ProcessNavigationForRootPage(nextSegment, segments, parameters, useModalNavigation, animated);
        }

        protected override Task ProcessNavigationForTabbedPage(TabbedPage currentPage, string nextSegment, Queue<string> segments,
            INavigationParameters parameters, bool? useModalNavigation, bool animated)
        {
            Debug.WriteLine($"ProcessNavigationForTabbedPage - {currentPage} - {nextSegment}");
            return base.ProcessNavigationForTabbedPage(currentPage, nextSegment, segments, parameters, useModalNavigation, animated);
        }

        protected override Task ProcessNavigationForContentPage(Page currentPage, string nextSegment, Queue<string> segments,
            INavigationParameters parameters, bool? useModalNavigation, bool animated)
        {
            Debug.WriteLine($"ProcessNavigationForContentPage - {currentPage} - {nextSegment}");
            return base.ProcessNavigationForContentPage(currentPage, nextSegment, segments, parameters, useModalNavigation, animated);
        }
    }
}