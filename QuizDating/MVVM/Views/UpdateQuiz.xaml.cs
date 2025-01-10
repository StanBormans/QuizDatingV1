using QuizDating.Data;
using QuizDating.Models;
using System.Collections.ObjectModel;

namespace QuizDating.MVVM.Views;

public partial class UpdateQuiz : ContentPage
{
    private Quiz _quiz;
    private readonly List<StackLayout> _questionEntries = new List<StackLayout>();
    public ObservableCollection<string> EffectOptions { get; set; } = new ObservableCollection<string>
    {
        "Outgoing", "Social", "Openness"
    };

    public UpdateQuiz(Quiz quiz)
    {
        InitializeComponent();
        BindingContext = this;

        var dbService = new LocalDbService();
        _quiz = dbService.GetQuizWithQuestions(quiz.Id).Result;

        QuizNameEntry.Text = _quiz.Name;

        foreach (var question in _quiz.Questions)
        {
            AddQuestionToLayout(question);
        }
    }

    private void AddQuestionToLayout(Question question)
    {
        var titleEntry = new Entry { Text = question.Title, Placeholder = "Edit question title", FontSize = 16 };
        var descriptionEntry = new Entry { Text = question.Description, Placeholder = "Edit question description", FontSize = 16 };
        var optionAEntry = new Entry { Text = question.OptionA, Placeholder = "Edit Option A", FontSize = 16 };
        var optionAEffectPicker = new Picker
        {
            Title = "Effect of Option A",
            ItemsSource = EffectOptions,
            SelectedItem = question.EffectA
        };
        var optionBEntry = new Entry { Text = question.OptionB, Placeholder = "Edit Option B", FontSize = 16 };
        var optionBEffectPicker = new Picker
        {
            Title = "Effect of Option B",
            ItemsSource = EffectOptions,
            SelectedItem = question.EffectB
        };

        var questionStack = new StackLayout
        {
            Spacing = 10,
            Children = { titleEntry, descriptionEntry, optionAEntry, optionAEffectPicker, optionBEntry, optionBEffectPicker }
        };

        QuestionsLayout.Children.Add(questionStack);
        _questionEntries.Add(questionStack);
    }

    private async void OnSaveChangesClicked(object sender, EventArgs e)
    {
        _quiz.Name = QuizNameEntry.Text;

        _quiz.Questions.Clear();
        foreach (var stack in _questionEntries)
        {
            var title = ((Entry)stack.Children[0]).Text;
            var description = ((Entry)stack.Children[1]).Text;
            var optionA = ((Entry)stack.Children[2]).Text;
            var optionAEffect = ((Picker)stack.Children[3]).SelectedItem as string;
            var optionB = ((Entry)stack.Children[4]).Text;
            var optionBEffect = ((Picker)stack.Children[5]).SelectedItem as string;

            _quiz.Questions.Add(new Question
            {
                Title = title,
                Description = description,
                OptionA = optionA,
                OptionB = optionB,
                EffectA = optionAEffect,
                EffectB = optionBEffect,
                QuizId = _quiz.Id
            });
        }

        var dbService = new LocalDbService();
        await dbService.UpdateQuiz(_quiz);

        await DisplayAlert("Success", "Quiz updated successfully!", "OK");
        Application.Current.MainPage = new QuizPage();
    }
}