using System;
using Prism;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;

namespace ConfApp.ViewModels
{
    public class ViewModelBase : BindableBase,
        INavigationAware,
        IDestructible,
        IPageLifecycleAware,
        IActiveAware,
        IInitialize
    {
        private bool _isActive;
        private string _title;

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        protected INavigationService NavigationService { get; }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, OnChanged);
        }

        public event EventHandler IsActiveChanged;

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

        public event EventHandler<bool> IsTabActiveChanged;

        private void OnChanged()
        {
            var isActive = IsActive;
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
            IsTabActiveChanged?.Invoke(this, isActive);
        }
    }
}