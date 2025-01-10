using QuizDating.Data;

namespace QuizDating.MVVM.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();

        // Load user data
        LoadUserProfile();
    }

    private void LoadUserProfile()
    {
        var user = SessionService.LoggedInUser;
        if (user != null)
        {
            UsernameLabel.Text = $"Username: {user.UserName}";

            if (user.CharacterResult != null)
            {
                OutgoingLabel.Text = $"Outgoing: {user.CharacterResult.Outgoing}";
                SocialLabel.Text = $"Social: {user.CharacterResult.Social}";
                OpennessLabel.Text = $"Openness: {user.CharacterResult.Opennes}";
            }
            else
            {
                OutgoingLabel.Text = "Outgoing: 0";
                SocialLabel.Text = "Social: 0";
                OpennessLabel.Text = "Openness: 0";
            }

            // Load the user's profile picture if set
            if (!string.IsNullOrEmpty(user.ProfilePicture))
            {
                ProfileImage.Source = user.ProfilePicture;
            }
        }
        else
        {
            UsernameLabel.Text = "No user logged in.";
            OutgoingLabel.Text = "Outgoing: N/A";
            SocialLabel.Text = "Social: N/A";
            OpennessLabel.Text = "Openness: N/A";
        }
    }

    private async void OnUpdateProfilePictureClicked(object sender, EventArgs e)
    {
        // Allow the user to pick a new profile picture
        var result = await FilePicker.PickAsync();
        if (result != null)
        {
            var user = SessionService.LoggedInUser;
            if (user != null)
            {
                user.ProfilePicture = result.FullPath;

                // Save the updated user data
                var dbService = new LocalDbService();
                await dbService.UpdateUser(user);

                // Update the displayed profile picture
                ProfileImage.Source = result.FullPath;

                await DisplayAlert("Success", "Profile picture updated successfully.", "OK");
            }
        }
    }

    private void OnBackClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage();
    }
}