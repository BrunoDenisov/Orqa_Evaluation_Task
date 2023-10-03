using OrqaDB.EntityFramework;  
using OrqaDB.WPF.Stores;
using System.Windows.Input;
using OrqaDB.EntityFramework.Queris;
using System.Collections.ObjectModel;
using OrqaDB.Domain.Models;
using System;
using OrqaDB.AuthenticationService;
using System.Windows;

namespace OrqaDB.WPF.ViewModels
{
    public class DbAdminDashViewModel : ViewModelBase
    {
        public UserWorkPositionListViewModel UserWorkPositionListViewModel { get; }
        public UserWorkPositionUpdateViewModel UserWorkPositionUpdateViewModel { get; }
        public UserCreationViewModel UserCreationViewModel { get; }
        public SelectedUserStore SelectedUserStore { get; }

        private readonly MainViewModel mainViewModel;

        private readonly AuthService authService;

        private readonly string accessToken;

        public ICommand LogoutButtonCommand { get; }

        public DbAdminDashViewModel(string accessToken, MainViewModel mainViewModel)
        {
            authService = new AuthService();
            this.accessToken = accessToken;

            SelectedUserStore = new SelectedUserStore();

            UserWorkPositionListViewModel = new UserWorkPositionListViewModel();
            UserWorkPositionUpdateViewModel = new UserWorkPositionUpdateViewModel();
            UserCreationViewModel = new UserCreationViewModel();

            UserCreationViewModel.UserCreated += OnUserCreated;
            UserWorkPositionUpdateViewModel.WorkPositionUpdated += OnWorkPositionUpdated;

            LogoutButtonCommand = new RelayCommand(LogoutCommandExecute);

            this.mainViewModel = mainViewModel;
        }

        public void LogoutCommandExecute(object parameter)
        {
            authService.Logout(accessToken);

            mainViewModel.NavigateToLogin();
        }


        private void OnUserCreated(object sender, EventArgs e)
        {
            UserWorkPositionListViewModel.RefreshUserWorkPositions();
        }

        private void OnWorkPositionUpdated(object sender, EventArgs e)
        {
            UserWorkPositionListViewModel.RefreshUserWorkPositions();
        }

    }
}
