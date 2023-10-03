using OrqaDB.AuthenticationService;
using System;
using System.Threading;
using System.Transactions.Configuration;
using System.Windows;

namespace OrqaDB.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string accessToken;

        private Timer tokenExpirationCheckTimer;

        private const int tokenExpirationCheckIntervalMinutes = 60;

        private readonly AuthService authService;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public MainViewModel()
        {
            CurrentViewModel = new LoginPageViewModel(accessToken);
            authService = new AuthService();

            var loginPageViewModel = CurrentViewModel as LoginPageViewModel;
            if (loginPageViewModel != null)
            {
                loginPageViewModel.OnLoginSuccess += OnLoginSuccess;
            }

        }

        public void OnLoginSuccess(string accessToken) // When a user succsfully logs in navigates them to the correct view based on their role
        {
            this.accessToken = accessToken;

            var role = TokenService.ExtractRoleFromAccessToken(accessToken);

            if (role == "Admin")
            {
                CurrentViewModel = new DbAdminDashViewModel(accessToken, this);
            }
            else if (role == "User")
            {
                CurrentViewModel = new UserLogedInViewModel(accessToken, this);
            }

            StartTokenExpirationCheckTimer();
        }


        public void NavigateToLogin()
        {
            CurrentViewModel = new LoginPageViewModel(accessToken);
        }

        private void StartTokenExpirationCheckTimer()
        {
            tokenExpirationCheckTimer = new Timer(TokenExpirationCheckCallback, null,
                                              TimeSpan.FromMinutes(tokenExpirationCheckIntervalMinutes),
                                              TimeSpan.FromMinutes(tokenExpirationCheckIntervalMinutes));
        }

        private void TokenExpirationCheckCallback(object state)
        {
            if (authService.IsAccessTokenValid(accessToken) == false)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Your session has expired. Please log in again.");
                    NavigateToLogin();
                });
            }
        }
    }

}
