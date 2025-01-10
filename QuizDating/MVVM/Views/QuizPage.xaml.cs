using QuizDating.MVVM.Views;
using QuizDating.Models;
using QuizDating.Data;
using QuizDating.MVVM.ViewModels;

namespace QuizDating;

public partial class QuizPage : ContentPage
{
    public QuizPage()
    {
        InitializeComponent();
        BindingContext = new QuizPageViewModel(); // Set ViewModel as BindingContext
        LoadQuizzes();
    }

    public void OnBackClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage();
    }

    public void OnCreateClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new CreateQuiz();
    }

    public async void LoadQuizzes()
    {
        var dbService = new LocalDbService();
        var quizzes = await dbService.GetQuizzesWithQuestions();

        var viewModel = BindingContext as QuizPageViewModel;
        if (viewModel != null)
        {
            viewModel.Quizzes = quizzes; // Bind quizzes to the UI
        }
    }

    private async void OnQuizOptionsClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Quiz quiz)
        {
            string action = await DisplayActionSheet($"Options for {quiz.Name}", "Cancel", null, "Update", "Delete");

            if (action == "Update")
            {
                Application.Current.MainPage = new UpdateQuiz(quiz); // Navigate to update quiz
            }
            else if (action == "Delete")
            {
                var dbService = new LocalDbService();
                await dbService.DeleteQuiz(quiz.Id);
                await DisplayAlert("Deleted", $"Quiz '{quiz.Name}' has been deleted.", "OK");
                LoadQuizzes();
            }
        }
    }

    private async void OnStartQuizClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Quiz quiz)
        {
            Application.Current.MainPage = new TakeQuizPage(quiz); // Navigate to take quiz page
        }
    }
}