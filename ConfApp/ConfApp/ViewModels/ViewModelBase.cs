using System;
using System.ComponentModel;
using System.Diagnostics;
using ConfApp.Services.Telemetry;
using ConfApp.Services.Telemetry.Events;
using Prism;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;

namespace ConfApp.ViewModels
{
    public class TabViewModelBase : ViewModelBase, IActiveAware
    {
        public bool IsInitialized { get; private set; }

        public TabViewModelBase(INavigationService navigationService, 
            IAnalyticsService analyticsService) : base(
            navigationService, analyticsService)
        {
            IsTabActiveChanged += OnIsTabActiveChanged;
            PropertyChanged += OnPropertyChanged;
            IsInitialized = false;

        }

        public bool IsActive { get; set; }

        public event EventHandler IsActiveChanged;

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(IsActive)) return;
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
            IsTabActiveChanged?.Invoke(this, IsActive);
        }

        /// <summary>
        ///     Make sure you call base.Initialize()S
        /// </summary>
        /// <param name="parameters"></param>
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            IsInitialized = true;
        }

        private void OnIsTabActiveChanged(object sender, bool e)
        {
            var status = IsActive ? "Active" : "Inactive";
            Debug.WriteLine($"ViewModelBase.OnIsTabActiveChanged()- {Title} Tab Is {status}");

            if (!IsInitialized) return;
            if (IsActive) TrackTabIsActive();
        }

        public event EventHandler<bool> IsTabActiveChanged;

        private void OnChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
            IsTabActiveChanged?.Invoke(this, IsActive);
        }

        private void TrackTabIsActive()
        {
            var message = "Tab was made is Visible (Active)";
            AnalyticsService
                .TrackEvent(new EventBase(message).AddParameter("TabName", Title));
        }

        public override void Destroy()
        {
            IsTabActiveChanged -= OnIsTabActiveChanged;
            PropertyChanged -= OnPropertyChanged;
            base.Destroy();
         
        }
    }

    public class ViewModelBase : BindableBase,
        INavigationAware,
        IDestructible,
        IPageLifecycleAware,
        IInitialize
    {
        public ViewModelBase(INavigationService navigationService,
            IAnalyticsService analyticsService)
        {
            AnalyticsService = analyticsService;
            NavigationService = navigationService;
        }

        protected IAnalyticsService AnalyticsService { get; }

        protected INavigationService NavigationService { get; }

        public string Title { get; set; }


        public virtual void Destroy()
        {
            Debug.WriteLine($"Detroying {Title} Page");
        }

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }
    }
}