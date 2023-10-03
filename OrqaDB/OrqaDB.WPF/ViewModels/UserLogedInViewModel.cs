using OrqaDB.AuthenticationService;
using OrqaDB.Domain.Models;
using OrqaDB.EntityFramework.DTOs;
using OrqaDB.EntityFramework.Queris;
using System;
using System.Windows;
using System.Windows.Input;

namespace OrqaDB.WPF.ViewModels
{
    public class UserLogedInViewModel : ViewModelBase
    {
        private readonly MainViewModel mainViewModel;
        private readonly OrqaDbContext _dbContext;
        private readonly AuthService authService;
        private readonly string accessToken;
        public ICommand LogoutButtonCommand { get; }

        private UserWorkPositionInfo _userWorkPositionInfo;
        public UserWorkPositionInfo UserWorkPositionInfo
        {
            get { return _userWorkPositionInfo; }
            set { _userWorkPositionInfo = value; OnPropertyChanged(nameof(UserWorkPositionInfo)); }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged(nameof(LastName)); }
        }

        private string workPosition;
        public string WorkPosition
        {
            get { return workPosition; }
            set { workPosition = value; OnPropertyChanged(nameof(WorkPosition)); }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(nameof(Date)); }
        }

        public UserLogedInViewModel(string accessToken, MainViewModel mainViewModel)
        {
            this.accessToken = accessToken;
            _dbContext = new OrqaDbContext();
            authService = new AuthService();

            var username = TokenService.ExtractUsernameFromAccessToken(accessToken);

            var userWorkPositionDto = new GetUserWorkPositionInfoQuery(_dbContext).GetUserWorkPositionInfo(username); // Fetching user info form database

            // Mapping fetched data to text blocks
            FirstName = userWorkPositionDto.FirstName;
            LastName = userWorkPositionDto.LastName;
            WorkPosition = userWorkPositionDto.WorkPosition;
            Date = userWorkPositionDto.Date;

            LogoutButtonCommand = new RelayCommand(LogoutCommandExecute);

            this.mainViewModel = mainViewModel;
        }

        public void LogoutCommandExecute(object parameter)
        {
            authService.Logout(accessToken);

            mainViewModel.NavigateToLogin();
        }
    }
}
