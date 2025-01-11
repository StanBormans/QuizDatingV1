using QuizDating.Data;
using QuizDating.Models;
using QuizDating.MVVM.ViewModels;

namespace QuizDating.MVVM.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(LocalDbService dbService)
    {
        InitializeComponent();
        BindingContext = new RegisterPageViewModel(dbService);
    }
}