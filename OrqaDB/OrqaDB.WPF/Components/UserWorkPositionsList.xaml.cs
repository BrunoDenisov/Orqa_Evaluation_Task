using OrqaDB.EntityFramework.Queris;
using OrqaDB.WPF.Stores;
using OrqaDB.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OrqaDB.WPF.Components
{
    /// <summary>
    /// Interaction logic for UserWorkPositionsList.xaml
    /// </summary>
    public partial class UserWorkPositionsList : UserControl
    {
        private readonly OrqaDbContext _dbContext;
        private readonly UserWorkPositionListViewModel _viewModel;
        private readonly UserWorkPositionUpdate _userWorkPositionUpdate;
        private readonly SelectedUserStore _selectedUserStore;
        private readonly UserWorkPositionUpdateViewModel _updateViewModel;

        public UserWorkPositionsList()
        {
            InitializeComponent();

            _dbContext = new OrqaDbContext();

            _viewModel = new UserWorkPositionListViewModel();

            _updateViewModel = new UserWorkPositionUpdateViewModel();

            _userWorkPositionUpdate = new UserWorkPositionUpdate();

            _selectedUserStore = new SelectedUserStore();

            _userWorkPositionUpdate.DataContext = _updateViewModel;

            DataContext = _viewModel;
        }


        private void RadioButton_Checked(object sender, RoutedEventArgs e) // When a radio button is checked it updates the SelectedUserStore with the apropriate data
        {
            if (sender is RadioButton radioButton && radioButton.DataContext is ListUserViewModel selectedUser)
            {
                SelectedUserStore.Instance.SelectedUserName = selectedUser.Username;
                SelectedUserStore.Instance.SelectedUserWorkPosition = selectedUser.WorkPosition;
                SelectedUserStore.Instance.PositionAssignedDate = selectedUser.PositionAssignedDate;
            }
        }


    }
}
