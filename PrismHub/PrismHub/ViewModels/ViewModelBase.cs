using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;

namespace PrismHub.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible, IPageLifecycleAware
    {
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