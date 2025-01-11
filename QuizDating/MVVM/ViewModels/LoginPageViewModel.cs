using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizDating.Data;
using QuizDating.MVVM.Views;
using QuizDating.Models;

namespace QuizDating.MVVM.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly LocalDbService _dbService;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string message;

        public LoginPageViewModel(LocalDbService dbService)
        {
            _dbService = dbService;
        }

        [RelayCommand]
        public async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                Message = "Please enter both username and password.";
                return;
            }

            try
            {
                var user = await _dbService.GetUserByUsername(Username);

                if (user != null && user.Password == Password)
                {
                    // Store the logged-in user in the session
                    SessionService.LoggedInUser = user;

                    // Navigate to MainPage
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    Message = "Invalid username or password. Try again.";
                }
            }
            catch (Exception ex)
            {
                Message = $"An error occurred: {ex.Message}";
            }
        }

        [RelayCommand]
        public void Register()
        {
            // Navigate to RegisterPage
            Application.Current.MainPage = new RegisterPage(_dbService);
        }
    }
}
