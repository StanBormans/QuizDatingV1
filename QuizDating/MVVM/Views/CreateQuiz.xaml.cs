using QuizDating.MVVM.Views;
using QuizDating.Models;
using QuizDating.Data;

namespace QuizDating.MVVM.Views;

public partial class CreateQuiz : ContentPage
{
    private List<Entry> _questionEntries = new List<Entry>();


    public CreateQuiz()
    {
        InitializeComponent();
    }

    public void OnAddQuestionClicked(object sender, EventArgs e)
    {
        var newEntry = new Entry { Placeholder = "Enter your question here" };
        _questionEntries.Add(newEntry);
        QuestionsLayout.Children.Insert(QuestionsLayout.Children.Count - 1, newEntry);
    }

    public async void OnSaveQuizClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(QuizNameEntry.Text))
        {
            await DisplayAlert("Error", "Please provide a name for the quiz.", "OK");
            return;
        }

        var quiz = new Quiz
        {
            Name = QuizNameEntry.Text
        };

        if (!string.IsNullOrWhiteSpace(InitialQuestionEntry.Text))
        {
            quiz.Questions.Add(new Question { Title = InitialQuestionEntry.Text });
        }

        foreach (var entry in _questionEntries)
        {
            if (!string.IsNullOrWhiteSpace(entry.Text))
            {
                quiz.Questions.Add(new Question { Title = entry.Text });
            }
        }

        var dbService = new LocalDbService();
        await dbService.CreateQuiz(quiz);

        await DisplayAlert("Success", "Quiz saved successfully!", "OK");
        Application.Current.MainPage = new QuizPage();
    }
}