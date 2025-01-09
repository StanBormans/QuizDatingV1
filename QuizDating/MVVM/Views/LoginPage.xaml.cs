namespace QuizDating.MVVM.Views;

public partial class LoginPage : ContentPage
{
    private readonly LocalDbService _dbService;

    public LoginPage()
	{
		InitializeComponent();
	}

    public void LoginClicked(object sender, EventArgs e)
    {
        string hardcodedUsername = "admin";
        string hardcodedPassword = "test";

        var username = UsernameEntry.Text;
        var password = PasswordEntry.Text;

        if (username == hardcodedUsername && password == hardcodedPassword)
        {
            // Navigate to MainPage
            Application.Current.MainPage = new MainPage();
        }
        else
        {
            MessageLabel.Text = "Invalid username or password. Try again.";
        }
    }

    public void RegisterClicked(object sender, EventArgs e)
    {
        // Navigate to RegisterPage
        Application.Current.MainPage = new RegisterPage(_dbService);
    }
}