using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OrqaDB.WPF.Stores
{
    public class SelectedUserStore : INotifyPropertyChanged
    {
        private string _selectedUserName;
        private string _selectedUserWorkPosition;
        private DateTime _positionAssignedDate;

        private static SelectedUserStore _instance;
        public static SelectedUserStore Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SelectedUserStore();
                return _instance;
            }
        }

        public string SelectedUserName
        {
            get { return _selectedUserName; }
            set
            {
                _selectedUserName = value;
                NotifyPropertyChanged(nameof(SelectedUserName));
                OnSelectedUserChanged();
            }
        }

        public string SelectedUserWorkPosition
        {
            get { return _selectedUserWorkPosition; }
            set
            {
                _selectedUserWorkPosition = value;
                NotifyPropertyChanged(nameof(SelectedUserWorkPosition));
                OnSelectedUserChanged();
            }
        }

        public DateTime PositionAssignedDate
        {
            get { return _positionAssignedDate; }
            set
            {
                _positionAssignedDate = value;
                NotifyPropertyChanged(nameof(PositionAssignedDate));
                OnSelectedUserChanged();
            }
        }

       
        public event EventHandler SelectedUserChanged;

        public void OnSelectedUserChanged()
        {
            SelectedUserChanged?.Invoke(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
