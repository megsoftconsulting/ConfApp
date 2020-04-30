using System;
using Prism;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;

namespace ConfApp.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible, IPageLifecycleAware, IActiveAware
    {
        private string _helloWorld;
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

        private void OnChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}