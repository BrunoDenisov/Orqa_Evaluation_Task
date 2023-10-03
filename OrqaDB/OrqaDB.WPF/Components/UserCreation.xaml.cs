using OrqaDB.Domain.Models;
using OrqaDB.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for UserCreation.xaml
    /// </summary>
    public partial class UserCreation : UserControl
    {
        private readonly UserCreationViewModel _viewModel;

        public UserCreation()
        {
            InitializeComponent();
            _viewModel = new UserCreationViewModel();
            DataContext = _viewModel;
        }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = _viewModel.FirstName;
            string lastName = _viewModel.LastName;
            string username = _viewModel.Username;
            string plainPassword = _viewModel.PlainPassword;
            WorkPosition workPostion = _viewModel.SelectedWorkPosition;

        }
    }

}
