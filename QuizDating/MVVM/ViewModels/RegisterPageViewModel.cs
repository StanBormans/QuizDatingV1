using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizDating.Data;
using QuizDating.Models;
using System.Threading.Tasks;
using QuizDating.MVVM.Views;

namespace QuizDating.MVVM.ViewModels
{
    public partial class RegisterPageViewModel : ObservableObject
    {
        private readonly LocalDbService _dbService;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string confirmPassword;

        [ObservableProperty]
        private string message;

        public RegisterPageViewModel(LocalDbService dbService)
        {
            _dbService = dbService;
        }

        [RelayCommand]
        public async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                Message = "One or more fields are missing.";
                return;
            }

            if (Password != ConfirmPassword)
            {
                Message = "Passwords do not match.";
                return;
            }

            try
            {
                await _dbService.CreateUser(new User
                {
                    UserName = Username,
                    Password = Password
                });

                Message = "User registered successfully.";

                // Navigate to LoginPage
                Application.Current.MainPage = new LoginPage(_dbService);
            }
            catch (Exception ex)
            {
                Message = $"An error occurred: {ex.Message}";
            }
        }
    }
}
