using QuizDating.Data;
using QuizDating.Models;

namespace QuizDating.MVVM.Views;


public partial class MatchPage : ContentPage
{

    private List<User> users = new List<User>();
    private int currentIndex = 0;

    public MatchPage()
    {
        InitializeComponent();
        LoadUsers();
    }

    private void LoadUsers()
    {
        // Hardcoded users
        users = new List<User>
        {
            new User
            {
                Id = 1,
                UserName = "Default User",
                ProfilePicture = "default_profile_pic.jpg",
                CharacterResult = new CharacterResult { Outgoing = 0, Social = 0, Opennes = 0 }
            },
            new User
            {
                Id = 2,
                UserName = "Alice",
                ProfilePicture = "alice.jpg",
                CharacterResult = new CharacterResult { Outgoing = 10, Social = 10, Opennes = 10 }
            },
            new User
            {
                Id = 3,
                UserName = "Bob",
                ProfilePicture = "bob.jpg",
                CharacterResult = new CharacterResult { Outgoing = 0, Social = 20, Opennes = 0 }
            },
            new User
            {
                Id = 4,
                UserName = "Carol",
                ProfilePicture = "Carol",
                CharacterResult = new CharacterResult { Outgoing = 0, Social = 0, Opennes = 0 }
            },
            new User
            {
                Id = 5,
                UserName = "Bas",
                ProfilePicture = "default_profile_pic.jpg",
                CharacterResult = new CharacterResult { Outgoing = 0, Social = 0, Opennes = 0 }
            }
        };

        // Filter users within the range of the logged-in user's CharacterResult
        var loggedInUser = SessionService.LoggedInUser;
        if (loggedInUser?.CharacterResult == null)
        {
            DisplayAlert("Error", "No character results found for the logged-in user.", "OK");
            Application.Current.MainPage = new MainPage();
            return;
        }

        users = users
            .Where(u =>
                Math.Abs(u.CharacterResult.Outgoing - loggedInUser.CharacterResult.Outgoing) <= 20 &&
                Math.Abs(u.CharacterResult.Social - loggedInUser.CharacterResult.Social) <= 20 &&
                Math.Abs(u.CharacterResult.Opennes - loggedInUser.CharacterResult.Opennes) <= 20)
            .ToList();

        if (users.Count > 0)
        {
            LoadCurrentUser();
        }
        else
        {
            DisplayAlert("No Matches", "No users match your criteria.", "OK");
            Application.Current.MainPage = new MainPage();
        }
    }

    private void LoadCurrentUser()
    {
        if (currentIndex < users.Count)
        {
            var currentUser = users[currentIndex];
            UsernameLabel.Text = currentUser.UserName;
            ProfileImage.Source = string.IsNullOrEmpty(currentUser.ProfilePicture)
                ? "default_profile_pic.jpg"
                : currentUser.ProfilePicture;
        }
        else
        {
            DisplayAlert("End", "No more profiles to show", "OK");
            Application.Current.MainPage = new MainPage();
        }
    }

    private void Like(object sender, EventArgs e)
    {
        HandleSwipe(true);
    }

    private void Dislike(object sender, EventArgs e)
    {
        HandleSwipe(false);
    }

    private void HandleSwipe(bool liked)
    {
        if (liked)
        {
            Console.WriteLine($"Liked {users[currentIndex].UserName}");
        }
        else
        {
            Console.WriteLine($"Disliked {users[currentIndex].UserName}");
        }

        currentIndex++;
        LoadCurrentUser();
    }

    private void OnBackClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage();
    }
}