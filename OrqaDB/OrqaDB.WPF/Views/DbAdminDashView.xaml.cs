using OrqaDB.WPF.Components;
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
using OrqaDB.EntityFramework;
using OrqaDB.WPF.Helpers;

namespace OrqaDB.WPF.Views
{
    /// <summary>
    /// Interaction logic for DbAdminDashView.xaml
    /// </summary>
    public partial class DbAdminDashView : UserControl
    {
        private readonly DbAdminDashViewModel _viewModel;

        public DbAdminDashView()
        {
            InitializeComponent();
            string accessToken = AccessTokenHolder.AccessToken;
            _viewModel = new DbAdminDashViewModel(accessToken, (MainViewModel)Application.Current.MainWindow.DataContext);
            DataContext = _viewModel;
        }
    }
}
