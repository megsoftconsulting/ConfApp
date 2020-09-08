﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConfApp.About;
using ConfApp.Services;
using ConfApp.Services.Telemetry;
using ConfApp.Speakers;
using ConfApp.Talks;
using ConfApp.ViewModels;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;

namespace ConfApp.Login
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly Lazy<HttpClient> _apiClient = new Lazy<HttpClient>(() => new HttpClient());
        private readonly IBrowser _browser;
        private readonly IPromptService _promptService;
        private readonly IAnalyticsService _analytics;
        private OidcClient _authClient;
        private LoginResult _result;

        public LoginViewModel(
            INavigationService navigationService,
            IAnalyticsService analytics,
            IBrowser browser,
            IPromptService promptService)
            : base(
                navigationService, analytics)
        {
            _analytics = analytics;
            _browser = browser;
            _promptService = promptService;
            SocialLoginCommand = new DelegateCommand<string>(OnSocialLogin);
            NoLoginCommand = new DelegateCommand(OnNoLogin);

            Title = "Login Page";
        }

        public DelegateCommand<string> SocialLoginCommand { get; set; }
        public DelegateCommand NoLoginCommand { get; set; }


        public override async void Initialize(INavigationParameters parameters)
        {
        }

        public override async void OnAppearing()
        {
            try
            {
                await Permissions.RequestAsync<Permissions.Maps>();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            base.OnAppearing();
        }

        private async void OnNoLogin()
        {
            _analytics.TrackEvent(new DidNotSignInEvent());

            var r = await NavigateToMainScreen();

            if (!r.Success)
            {
                await _promptService.DisplayAlert("Error", "Error while trying to navigate to the main page.", "OK");
                _analytics.TrackError(r.Exception);
                Debug.WriteLine(r.Exception);
            }
        }

        private async Task<INavigationResult> NavigateToMainScreen()
        {
            var sb = new StringBuilder("/MainPage?");
            sb.Append($"createTab={nameof(BigTitleNavigationPage)}|{nameof(SpeakersPage)}");
            sb.Append($"&createTab={nameof(BigTitleNavigationPage)}|{nameof(TalksPage)}");
            sb.Append($"&createTab={nameof(BigTitleNavigationPage)}|{nameof(AboutPage)}");
            //sb.Append($"&createTab={nameof(BigTitleNavigationPage)}|{nameof(ContentPage)}");
            var r = await NavigationService.NavigateAsync(sb.ToString());
            return r;
        }

        private async void OnSocialLogin(string scheme)
        {
           // var r = await _authClient.LoginAsync();
            var e = new SignedInEvent(scheme);
            _analytics.TrackEvent(e);
            _analytics.SetCurrentUser("Claudio Sanchez", "claudio@megsoftconsulting.com");
            await NavigateToMainScreen();
        }
    }
}