using QuizDating.Data;

namespace QuizDating.MVVM.Views;

public partial class LoginPage : ContentPage
{
    private readonly LocalDbService _dbService;

    public LoginPage(LocalDbService dbService)
    {
        _dbService = dbService;
        InitializeComponent();
    }

    public async void LoginClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text;
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            MessageLabel.Text = "Please enter both username and password.";
            return;
        }

        try
        {
            var user = await _dbService.GetUserByUsername(username);

            if (user != null && user.Password == password)
            {
                // Store the logged-in user in the session
                SessionService.LoggedInUser = user;

                // Navigate to MainPage
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                MessageLabel.Text = "Invalid username or password. Try again.";
            }
        }
        catch (Exception ex)
        {
            MessageLabel.Text = $"An error occurred: {ex.Message}";
        }
    }

    public void RegisterClicked(object sender, EventArgs e)
    {
        // Navigate to RegisterPage
        Application.Current.MainPage = new RegisterPage(_dbService);
    }
}