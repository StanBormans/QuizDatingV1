using QuizDating.MVVM.Views;
using QuizDating.Models;
using QuizDating.Data;
using System.Collections.ObjectModel;

namespace QuizDating.MVVM.Views;

public partial class CreateQuiz : ContentPage
{
    private List<StackLayout> _questionEntries = new List<StackLayout>();
    public ObservableCollection<string> EffectOptions { get; set; } = new ObservableCollection<string>
    {
        "Outgoing", "Social", "Openness"
    };

    public CreateQuiz()
    {
        InitializeComponent();
        BindingContext = this; // Bind EffectOptions to the page
    }

    public void OnAddQuestionClicked(object sender, EventArgs e)
    {
        // Create fields for a new question
        var titleEntry = new Entry { Placeholder = "Enter question title", FontSize = 16 };
        var descriptionEntry = new Entry { Placeholder = "Enter question description", FontSize = 16 };
        var optionAEntry = new Entry { Placeholder = "Enter Option A", FontSize = 16 };
        var optionAEffectPicker = new Picker { Title = "Effect of Option A", ItemsSource = EffectOptions };
        var optionBEntry = new Entry { Placeholder = "Enter Option B", FontSize = 16 };
        var optionBEffectPicker = new Picker { Title = "Effect of Option B", ItemsSource = EffectOptions };

        // Create a container for the fields
        var questionStack = new StackLayout
        {
            Spacing = 10,
            Children =
            {
                titleEntry, descriptionEntry, optionAEntry, optionAEffectPicker, optionBEntry, optionBEffectPicker
            }
        };

        // Add to layout and track entries
        QuestionsLayout.Children.Insert(QuestionsLayout.Children.Count - 1, questionStack);
        _questionEntries.Add(questionStack);
    }

    public async void OnSaveQuizClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(QuizNameEntry.Text))
        {
            await DisplayAlert("Error", "Quiz name cannot be empty.", "OK");
            return;
        }

        // Create quiz object
        var quiz = new Quiz { Name = QuizNameEntry.Text };

        // Add the first question
        if (!string.IsNullOrWhiteSpace(InitialQuestionEntry.Text))
        {
            var optionAEffect = InitialOptionAEffectPicker.SelectedItem as string;
            var optionBEffect = InitialOptionBEffectPicker.SelectedItem as string;

            quiz.Questions.Add(new Question
            {
                Title = InitialQuestionEntry.Text,
                Description = InitialDescriptionEntry.Text,
                OptionA = InitialOptionAEntry.Text,
                OptionB = InitialOptionBEntry.Text,
                EffectA = optionAEffect,
                EffectB = optionBEffect
            });
        }

        // Add dynamically created questions
        foreach (var stack in _questionEntries)
        {
            var title = ((Entry)stack.Children[0]).Text;
            var description = ((Entry)stack.Children[1]).Text;
            var optionA = ((Entry)stack.Children[2]).Text;
            var optionAEffect = ((Picker)stack.Children[3]).SelectedItem as string;
            var optionB = ((Entry)stack.Children[4]).Text;
            var optionBEffect = ((Picker)stack.Children[5]).SelectedItem as string;

            quiz.Questions.Add(new Question
            {
                Title = title,
                Description = description,
                OptionA = optionA,
                OptionB = optionB,
                EffectA = optionAEffect,
                EffectB = optionBEffect
            });
        }

        // Save to database
        var dbService = new LocalDbService();
        await dbService.CreateQuiz(quiz);

        await DisplayAlert("Success", "Quiz saved successfully!", "OK");
        Application.Current.MainPage = new QuizPage();
    }

    private int GetEffectValue(object selectedEffect)
    {
        return selectedEffect switch
        {
            "Outgoing" => 10,
            "Social" => 10,
            "Openness" => 10,
            _ => 0
        };
    }
}