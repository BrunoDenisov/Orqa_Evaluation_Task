using OrqaDB.Domain.Models;
using OrqaDB.EntityFramework.Queris;
using OrqaDB.WPF.ViewModels;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using OrqaDB.EntityFramework.Queries;
using System.Windows;

namespace OrqaDB.WPF.ViewModels
{
    public class UserCreationViewModel : ViewModelBase
    {
        public ICommand UserCreationCommand { get; }
        public ICommand GenerateRandomPasswordCommand { get; }
        public ICommand EditWorkPositionCommand { get; set; }
        public string GeneratedPassword { get; private set; }

        public event EventHandler UserCreated;

        private readonly OrqaDbContext _dbContext;

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _plainPassword;
        public string PlainPassword
        {
            get { return _plainPassword; }
            set
            {
                _plainPassword = value;
                OnPropertyChanged(nameof(PlainPassword));
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

        private List<string> _availableRoles;
        public List<string> AvailableRoles
        {
            get { return _availableRoles; }
            set
            {
                _availableRoles = value;
                OnPropertyChanged(nameof(AvailableRoles));
            }
        }

        private string _selectedRole;
        public string SelectedRole
        {
            get { return _selectedRole; }
            set
            {
                _selectedRole = value;
                OnPropertyChanged(nameof(SelectedRole));
            }
        }

        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }


        public UserCreationViewModel()
        {
            _dbContext = new OrqaDbContext();
            UserCreationCommand = new RelayCommand(async (parameter) => await CreateUserAsync());
            GenerateRandomPasswordCommand = new RelayCommand(GenerateRandomPassword);

            FetchAvailableWorkPositions();
            FetchAvailableRoles();
        }

        private async Task CreateUserAsync()
        {
            var firstName = FirstName;
            var lastName = LastName;
            var username = Username;
            var plainPassword = PlainPassword;
            var roleName = SelectedRole;
            var productName = ProductName; 

            if (!string.IsNullOrEmpty(GeneratedPassword)) // If the generate passowrd is in the text box it uses it
            {
                plainPassword = GeneratedPassword;
            }

            string hashedPassword = HashPassword(plainPassword);

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = hashedPassword
            };

            try
            {
                await InsertUserQuery.InsertUserAsync(user, _dbContext, roleName);

                await InsertUserWorkPositionQuery.InsertUserWorkPositionAsync(_dbContext, user.Username, SelectedWorkPosition, productName);

                Console.WriteLine("User created successfully!");
                MessageBox.Show("User and work position created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                OnUserCreated();

                MessageBox.Show("User created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating user: " + ex.Message);
                MessageBox.Show("An error occurred while creating the user and work position. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string HashPassword(string password) // Method for hashing a password
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "");
            }
        }

        private void GenerateRandomPassword(object obj) // Method for generating a random 12 char password and updating the password text box with it
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();

            var password = new StringBuilder();

            for (int i = 0; i < 12; i++)
            {
                var index = random.Next(validChars.Length);
                password.Append(validChars[index]);
            }

            PlainPassword= password.ToString();
        }

        private void FetchAvailableWorkPositions()
        {
            using (var workPositionQueries = new FetchAviableWorkPositionsFromDB(_dbContext))
            {
                AvailableWorkPositions = workPositionQueries.GetAvailableWorkPositions();
            }
        }

        private void FetchAvailableRoles()
        {
            using (var rolesQuery = new FetchAvailableRolesFromDB(_dbContext))
            {
                AvailableRoles = rolesQuery.GetAvailableRoles();
            }
        }

        private void OnUserCreated()
        {
            UserCreated?.Invoke(this, EventArgs.Empty);
        }
    }
}


