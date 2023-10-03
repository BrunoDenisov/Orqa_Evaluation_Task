using OrqaDB.WPF.Stores;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using OrqaDB.EntityFramework;
using OrqaDB.EntityFramework.DTOs;
using OrqaDB.EntityFramework.Queris;
using System.Collections.Generic;
using System.Linq;

namespace OrqaDB.WPF.ViewModels
{
    public class UserWorkPositionListViewModel : ViewModelBase
    {
        private readonly OrqaDbContext _dbContext;

        //public ICommand EditWorkPositionCommand { get; set; }

        private ObservableCollection<ListUserViewModel> _userWorkPositions;
        public ObservableCollection<ListUserViewModel> UserWorkPositions
        {
            get { return _userWorkPositions; }
            set
            {
                _userWorkPositions = value;
                OnPropertyChanged(nameof(UserWorkPositions));
            }
        }
        private ListUserViewModel _selectedUser;
        public ListUserViewModel SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; OnPropertyChanged(nameof(SelectedUser)); }
        }

        private SelectedUserStore _selectedUserStore;
        public SelectedUserStore SelectedUserStore
        {
            get { return _selectedUserStore; }
            set
            {
                _selectedUserStore = value;
                OnPropertyChanged(nameof(SelectedUserStore));
            }
        }

        private ObservableCollection<ListUserViewModel> ConvertToViewModels(IEnumerable<UserWorkPositionDto> users)
        {
            return new ObservableCollection<ListUserViewModel>(users.Select(ConvertToViewModel));
        }

        public UserWorkPositionListViewModel()
        {
            _dbContext = new OrqaDbContext();

            //EditWorkPositionCommand = new RelayCommand(EditWorkPosition);

            _selectedUserStore = SelectedUserStore.Instance;

            LoadDataFromDatabase();

        }

        
        private void LoadDataFromDatabase() // Initial load aviable WorkPostions from the data base
        {
            var users = new AdminGetAllUsersQuery(_dbContext).GetAllUsers();

            UserWorkPositions = ConvertToViewModels(users);
        }

        //private void EditWorkPosition(object parameter)
        //{
        //    SelectedUser = parameter as ListUserViewModel;

        //    Console.WriteLine("Selected User: " + SelectedUser.WorkPosition);  

        //    foreach (var user in UserWorkPositions)
        //    {
        //        user.IsSelected = user == SelectedUser;
        //    }
        //}

        private ListUserViewModel ConvertToViewModel(UserWorkPositionDto userWorkPostionDto) // Convering fetched data form the data base to the apropriate format for displaying it
        {
            var userDto = userWorkPostionDto.User;

            if (userDto != null)
            {
                return new ListUserViewModel(
                    userDto.FirstName,
                    userDto.LastName,
                    userWorkPostionDto.Username,
                    userWorkPostionDto.WorkPosition,
                    userWorkPostionDto.Date);
            }

            return null;
        }

        public void RefreshUserWorkPositions()
        {
            LoadDataFromDatabase();
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }
    }
}
