using OrqaDB.WPF.Helpers;
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

namespace OrqaDB.WPF.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    ///
    public partial class LoginPage : UserControl
    {
        private readonly LoginPageViewModel _viewModel;

        public LoginPage()
        {
            InitializeComponent();
            string accessToken = AccessTokenHolder.AccessToken;
            _viewModel = new LoginPageViewModel(accessToken);
            DataContext = _viewModel;
        }
    }
}
