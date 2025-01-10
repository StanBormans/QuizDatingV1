using QuizDating.Data;
using QuizDating.Models;

namespace QuizDating.MVVM.Views;

public partial class UpdateQuiz : ContentPage
{
    private Quiz _quiz;
    private List<Entry> _questionEntries = new List<Entry>();

    public UpdateQuiz(Quiz quiz)
    {
        InitializeComponent();
        _quiz = quiz;

        QuizNameEntry.Text = _quiz.Name;

        foreach (var question in _quiz.Questions)
        {
            var questionEntry = new Entry
            {
                Text = question.Title,
                Placeholder = "Edit question here",
                FontSize = 16
            };
            _questionEntries.Add(questionEntry);
            QuestionsLayout.Children.Add(questionEntry);
        }
    }

    public async void OnSaveChangesClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(QuizNameEntry.Text))
        {
            await DisplayAlert("Error", "Quiz name cannot be empty.", "OK");
            return;
        }

        _quiz.Name = QuizNameEntry.Text;

        for (int i = 0; i < _quiz.Questions.Count; i++)
        {
            _quiz.Questions[i].Title = _questionEntries[i].Text;
        }

        var dbService = new LocalDbService();
        await dbService.UpdateQuiz(_quiz);

        await DisplayAlert("Success", "Quiz updated successfully!", "OK");
        Application.Current.MainPage = new QuizPage();
    }
}