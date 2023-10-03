using OrqaDB.WPF.Stores;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using OrqaDB.EntityFramework;
using OrqaDB.EntityFramework.Queris;
using OrqaDB.Domain.Models;
using OrqaDB.EntityFramework.Queries;
using System.Windows;

namespace OrqaDB.WPF.ViewModels
{
    public class UserWorkPositionUpdateViewModel : ViewModelBase
    {
        private readonly OrqaDbContext _dbContext;
        private readonly SelectedUserStore _selectedUserStore;

        public event EventHandler WorkPositionUpdated;

        public ICommand UpdateCommand { get; set; }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string _currentWorkPosition;
        public string CurrentWorkPosition
        {
            get { return _currentWorkPosition; }
            set
            {
                _currentWorkPosition = value;
                OnPropertyChanged(nameof(CurrentWorkPosition));
            }
        }

        private WorkPosition _selectedWorkPosition;
        public WorkPosition SelectedWorkPosition
        {
            get { return _selectedWorkPosition; }
            set
            {
                _selectedWorkPosition = value;
                OnPropertyChanged(nameof(SelectedWorkPosition));
            }
        }

        private DateTime _dateUpdated;
        public DateTime DateUpdated
        {
            get { return _dateUpdated; }
            set
            {
                _dateUpdated = value;
                OnPropertyChanged(nameof(DateUpdated));
            }
        }

        private List<WorkPosition> _availableWorkPositions;
        public List<WorkPosition> AvailableWorkPositions
        {
            get { return _availableWorkPositions; }
            set
            {
                _availableWorkPositions = value;
                OnPropertyChanged(nameof(AvailableWorkPositions));
            }
        }


        public UserWorkPositionUpdateViewModel()
        {
            _selectedUserStore = SelectedUserStore.Instance;
            _selectedUserStore.SelectedUserChanged += OnSelectedUserChanged;
            UpdateCommand = new RelayCommand(UpdateWorkPosition);
            _dbContext = new OrqaDbContext();

            FetchAvailableWorkPositions();
        }

        private void OnSelectedUserChanged(object sender, EventArgs e)
        {
            string selectedUsername = _selectedUserStore.SelectedUserName;
            string currentWorkPosition = _selectedUserStore.SelectedUserWorkPosition;
            DateTime positionAssignedDate = _selectedUserStore.PositionAssignedDate;


            UserName = selectedUsername;
            CurrentWorkPosition = currentWorkPosition;
            DateUpdated = positionAssignedDate;


            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(CurrentWorkPosition));
            OnPropertyChanged(nameof(DateUpdated));
        }

        private async void UpdateWorkPosition(object parameter)
        {
            try
            {
                string userName = UserName;
                string selectedWorkPosition = SelectedWorkPosition.PositionName;

                var updateQuery = new UpdateUserWorkPositionQuery(_dbContext);
                await updateQuery.UpdateWorkPositionAsync(userName, selectedWorkPosition);

                OnWorkPositionUpdated();
                MessageBox.Show("Work position updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to update work position: " + ex.Message);
                MessageBox.Show("Failed to update work position: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }


        private void FetchAvailableWorkPositions()
        {
            using (var workPositionQueries = new FetchAviableWorkPositionsFromDB(_dbContext))
            {
                AvailableWorkPositions = workPositionQueries.GetAvailableWorkPositions();
            }
        }

        private void OnWorkPositionUpdated()
        {
            WorkPositionUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
