using QuizDating.Models;

namespace QuizDating.MVVM.Views;

public partial class RegisterPage : ContentPage
{
    private readonly LocalDbService _dbService;
    private int _id;

    public RegisterPage(LocalDbService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
    }

    public async void OnRegisterClicked(object sender, EventArgs e)
    {
        
        var Username = UsernameEntry.Text;
        var Password = PasswordEntry.Text;
        var ConfirmPassword = ConfirmPasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
        {
            MessageLabel.Text = "One or more fields are missing.";
            return;
        }

        if (Password == ConfirmPassword)
        {
            try
            {
                if (_id == 0)
                {
                    if (_dbService != null)
                    {
                        await _dbService.CreateUser(new User
                        {
                            UserName = Username,
                            Password = Password
                        });

                        MessageLabel.Text = "User registered successfully.";
                    }
                    else
                    {
                        MessageLabel.Text = "Database service is unavailable.";
                    }
                }
                else
                {
                    MessageLabel.Text = "Invalid operation: User ID should be 0 for new users.";
                }
            }
            catch (Exception ex)
            {
                MessageLabel.Text = $"An error occurred: {ex.Message}";
            }
            finally
            {
                _id = 0;
            }
        }
        else
        {
            MessageLabel.Text = "Passwords do not match.";
        }
    }
}