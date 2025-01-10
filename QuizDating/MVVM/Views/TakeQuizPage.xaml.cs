using QuizDating.Data;
using QuizDating.Models;

namespace QuizDating.MVVM.Views;

public partial class TakeQuizPage : ContentPage
{
    private readonly Quiz _quiz;
    private readonly CharacterResult _characterResult = new CharacterResult();
    private readonly Dictionary<Question, string> _userAnswers = new Dictionary<Question, string>();

    public TakeQuizPage(Quiz quiz)
    {
        InitializeComponent();

        // Load the quiz with its CharacterResult
        var dbService = new LocalDbService();
        _quiz = dbService.GetQuizWithQuestions(quiz.Id).Result;

        // Ensure CharacterResult is not null
        _characterResult = _quiz.CharacterResult ?? new CharacterResult();

        LoadQuestions();
    }

    private void LoadQuestions()
    {
        foreach (var question in _quiz.Questions)
        {
            // Question Title
            var titleLabel = new Label
            {
                Text = question.Title,
                FontSize = 18,
                TextColor = Colors.Black
            };

            // Question Description
            var descriptionLabel = new Label
            {
                Text = question.Description,
                FontSize = 14,
                TextColor = Colors.Gray
            };

            // Option A Button
            var optionAButton = new Button
            {
                Text = question.OptionA,
                BackgroundColor = Colors.LightGray,
                TextColor = Colors.White,
                CommandParameter = question
            };
            optionAButton.Clicked += (s, e) => OnOptionSelected(s, e, question, "A");

            // Option B Button
            var optionBButton = new Button
            {
                Text = question.OptionB,
                BackgroundColor = Colors.LightGray,
                TextColor = Colors.White,
                CommandParameter = question
            };
            optionBButton.Clicked += (s, e) => OnOptionSelected(s, e, question, "B");

            // Add to layout
            QuestionsLayout.Children.Add(titleLabel);
            QuestionsLayout.Children.Add(descriptionLabel);
            QuestionsLayout.Children.Add(optionAButton);
            QuestionsLayout.Children.Add(optionBButton);

            // Track buttons for visual updates
            question.OptionAButton = optionAButton;
            question.OptionBButton = optionBButton;
        }
    }

    private void OnOptionSelected(object sender, EventArgs e, Question question, string selectedOption)
    {
        // Reset button colors
        question.OptionAButton.BackgroundColor = Colors.LightGray;
        question.OptionBButton.BackgroundColor = Colors.LightGray;

        // Highlight the selected button
        if (selectedOption == "A")
        {
            question.OptionAButton.BackgroundColor = Colors.LightGreen;
        }
        else if (selectedOption == "B")
        {
            question.OptionBButton.BackgroundColor = Colors.LightBlue;
        }

        // Update the user's answer
        if (_userAnswers.ContainsKey(question))
        {
            _userAnswers[question] = selectedOption;
        }
        else
        {
            _userAnswers.Add(question, selectedOption);
        }
    }

    private async void OnFinishQuizClicked(object sender, EventArgs e)
    {
        // Calculate final CharacterResult based on user answers
        foreach (var (question, answer) in _userAnswers)
        {
            if (answer == "A")
            {
                UpdateCharacterResult(question.EffectA);
            }
            else if (answer == "B")
            {
                UpdateCharacterResult(question.EffectB);
            }
        }

        // Save the character result
        var dbService = new LocalDbService();
        await dbService.UpdateCharacterResult(_characterResult);

        // Show result to the user
        await DisplayAlert("Quiz Complete",
            $"Outgoing: {_characterResult.Outgoing}\nSocial: {_characterResult.Social}\nOpenness: {_characterResult.Opennes}",
            "OK");

        // Navigate back to QuizPage
        Application.Current.MainPage = new QuizPage();
    }

    private void UpdateCharacterResult(string effect)
    {
        switch (effect)
        {
            case "Outgoing":
                _characterResult.Outgoing += 10;
                break;
            case "Social":
                _characterResult.Social += 10;
                break;
            case "Openness":
                _characterResult.Opennes += 10;
                break;
            default:
                break;
        }
    }
}