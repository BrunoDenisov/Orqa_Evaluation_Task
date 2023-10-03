using OrqaDB.AuthenticationService;
using System.Windows.Input;
using System.Windows;
using System;
using System.Security;
using OrqaDB.WPF.Views;
using OrqaDB.WPF.Helpers;

namespace OrqaDB.WPF.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly AuthService authService;

        public event Action<string> OnLoginSuccess;

        public string AccessToken { get; private set; }

        public ICommand LoginButtonCommand { get; }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private SecureString password;
        public SecureString Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public LoginPageViewModel(string accessToken)
        {
            this.AccessToken = accessToken;
            authService = new AuthService();

            LoginButtonCommand = new RelayCommand(delegate { Login(); });
        }

        private void Login()
        {
            try
            {
                if (Password == null)
                {
                    MessageBox.Show("Password cannot be null.");
                    return;
                }

                string plainPassword = ConvertSecureStringToString(Password);

                SecurePasswordHelper.ClearSecureString(Password); // Cleaning plain password form memory

                string accessToken = authService.Authenticate(Username, plainPassword);

                AccessTokenHolder.AccessToken = accessToken;

                if (!string.IsNullOrEmpty(accessToken))
                {
                    var role = TokenService.ExtractRoleFromAccessToken(accessToken);

                    MainViewModel mainViewModel = (MainViewModel)Application.Current.MainWindow.DataContext;

                    ((MainViewModel)Application.Current.MainWindow.DataContext).OnLoginSuccess(accessToken); // Navigating based on the role

                    OnLoginSuccess?.Invoke(accessToken);
                }
                else
                {
                    MessageBox.Show("Login failed. Invalid credentials.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login failed: " + ex.Message);
            }
        }

        private string ConvertSecureStringToString(SecureString secureString)
        {
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureString);
            try
            {
                return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
        }
    }
}
