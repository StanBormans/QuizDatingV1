using QuizDating.Models;
using QuizDating.MVVM.Views;

namespace QuizDating;


public partial class MatchPage : ContentPage
{

    private List<User> users;
    private int currentIndex = 0;

    public MatchPage()
    {
        InitializeComponent();
        users = new List<User>
            {
                new User { Id = 1, UserName = "Alice", ProfilePicture = "alice.jpg" },
                new User { Id = 2, UserName = "Bob", ProfilePicture = "bob.jpg" },
                new User { Id = 3, UserName = "Carol", ProfilePicture = "carol.jpg" }
            };

        LoadCurrentUser();
    }

    public void Like()
    {
        HandleSwipe(true);
    }

    public void Dislike()
    {
        HandleSwipe(false);
    }

    private void LoadCurrentUser()
    {
        if (currentIndex < users.Count)
        {

        }
        else
        {
            Application.Current.MainPage.DisplayAlert("End", "No more profiles to show", "OK");
            Application.Current.MainPage = new MainPage();
        }
    }

    private void HandleSwipe(bool liked)
    {
        if (liked)
        {
            // Handle match logic
        }

        currentIndex++;
        LoadCurrentUser();
    }
}