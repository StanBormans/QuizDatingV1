using QuizDating.MVVM.Views;

namespace QuizDating;

public partial class QuizPage : ContentPage
{
    public QuizPage()
    {
        InitializeComponent();
    }

    public void OnBackClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage();
    }
}