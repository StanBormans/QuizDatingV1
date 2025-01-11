using QuizDating.Data;
using QuizDating.MVVM.ViewModels;

namespace QuizDating.MVVM.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LocalDbService dbService)
    {
        InitializeComponent();
        BindingContext = new LoginPageViewModel(dbService);
    }
}