using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrqaDB.WPF.ViewModels
{
    public class ListUserViewModel : ViewModelBase
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string WorkPosition { get; set; }

        public DateTime PositionAssignedDate { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public ListUserViewModel(string firstName, string lastName, string username, string workPosition, DateTime positionAssignedDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            WorkPosition = workPosition;
            PositionAssignedDate = positionAssignedDate;
        }
    }
}
