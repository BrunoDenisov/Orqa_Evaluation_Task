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
using OrqaDB.WPF.Helpers;
using OrqaDB.WPF.ViewModels;

namespace OrqaDB.WPF.Views
{
    /// <summary>
    /// Interaction logic for UserLogedInView.xaml
    /// </summary>
    public partial class UserLogedInView : UserControl
    {
        private readonly UserLogedInViewModel _viewModel;
       
        public UserLogedInView()
        {
            InitializeComponent();
            string accessToken = AccessTokenHolder.AccessToken;
            _viewModel = new UserLogedInViewModel(accessToken, (MainViewModel)Application.Current.MainWindow.DataContext);
            DataContext = _viewModel;
        }
    }
}
