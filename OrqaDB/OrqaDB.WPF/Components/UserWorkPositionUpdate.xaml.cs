using OrqaDB.WPF.Stores;
using OrqaDB.WPF.ViewModels;
using System;
using System.Windows.Controls;

namespace OrqaDB.WPF.Components
{
    public partial class UserWorkPositionUpdate : UserControl
    {
        private readonly UserWorkPositionUpdateViewModel _viewModel;

        private readonly SelectedUserStore _selectedUserStore;

        private readonly OrqaDbContext _dbContext;

        public UserWorkPositionUpdate()
        {
            _dbContext = new OrqaDbContext();

            _selectedUserStore = new SelectedUserStore();

            InitializeComponent();
            _viewModel = new UserWorkPositionUpdateViewModel();
            DataContext = _viewModel;
        }

    }
}