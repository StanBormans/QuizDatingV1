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
        BindingContext = new QuizPageViewModel(); // Ensure the ViewModel is set as the BindingContext
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
            viewModel.Quizzes = quizzes; // Update the Quizzes property in the ViewModel
        }
    }

    private async void OnQuizOptionsClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Quiz quiz)
        {
            string action = await DisplayActionSheet($"Opties voor {quiz.Name}", "Cancel", null, "Update", "Delete");

            if (action == "Update")
            {
                var dbService = new LocalDbService();
                await dbService.UpdateQuiz(quiz);
                Application.Current.MainPage = new UpdateQuiz(quiz);
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
}