using System;
using System.Diagnostics;
using ConfApp.Services.Telemetry;
using Prism;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;

namespace ConfApp.ViewModels
{
    public class TabViewModelBase : ViewModelBase, IActiveAware
    {
        private bool _isActive;
        private bool _wasInitialized;

        public TabViewModelBase(INavigationService navigationService, ITelemetryService telemetryService) : base(
            navigationService, telemetryService)
        {
            IsTabActiveChanged += OnIsTabActiveChanged;
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                SetProperty(ref _isActive, value);
                OnChanged();
            }
        }

        public event EventHandler IsActiveChanged;

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            _wasInitialized = true;
        }

        private void OnIsTabActiveChanged(object sender, bool e)
        {
            var status = IsActive ? "Active" : "Inactive";
            Debug.WriteLine($"ViewModelBase - {Title} Tab Is {status}");

            if (!_wasInitialized) return;
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
            var message = $"Tab {Title} was Tabbed";
            TelemetryService
                .TrackEvent(new EventBase(message));
            Debug.WriteLine(message);
        }
    }

    public class ViewModelBase : BindableBase,
        INavigationAware,
        IDestructible,
        IPageLifecycleAware,
        IInitialize
    {
        private string _title;

        public ViewModelBase(INavigationService navigationService,
            ITelemetryService telemetryService)
        {
            TelemetryService = telemetryService;
            NavigationService = navigationService;
        }

        protected ITelemetryService TelemetryService { get; }

        protected INavigationService NavigationService { get; }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }


        public virtual void Destroy()
        {
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