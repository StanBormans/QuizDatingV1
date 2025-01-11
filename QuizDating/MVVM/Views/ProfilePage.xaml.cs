using Azure;
using Microsoft.Extensions.Configuration;
using QuizDating.Data;
using System.IO;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace QuizDating.MVVM.Views;

public partial class ProfilePage : ContentPage
{
    private readonly string _apiKey = "EVb51yugKiowKHysnATpaSlRHBgYIYCMelkCKtpUTmkYeCtdR7pTJQQJ99BAAC5RqLJXJ3w3AAAFACOGjReh";
    private readonly string _endpoint = "https://computer-vision-api-quizdating.cognitiveservices.azure.com/";

    public ProfilePage()
    {
        InitializeComponent();

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
        var result = await FilePicker.PickAsync();
        if (result == null) return;

        try
        {
            bool isSafe = await AnalyzeImageForExplicitContent(result.FullPath);
            if (!isSafe)
            {
                await DisplayAlert("Warning", "The selected image contains inappropriate content. Please choose a different image.", "OK");
                return;
            }

            var user = SessionService.LoggedInUser;
            if (user != null)
            {
                user.ProfilePicture = result.FullPath;

                var dbService = new LocalDbService();
                await dbService.UpdateUser(user);

                ProfileImage.Source = result.FullPath;
                await DisplayAlert("Success", "Profile picture updated successfully.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private async Task<bool> AnalyzeImageForExplicitContent(string imagePath)
    {
        var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_apiKey))
        {
            Endpoint = _endpoint
        };

        var features = new List<VisualFeatureTypes?> { VisualFeatureTypes.Adult };

        using var imageStream = File.OpenRead(imagePath);
        ImageAnalysis analysisResult = await client.AnalyzeImageInStreamAsync(imageStream, features);

        return !(analysisResult.Adult.IsAdultContent || analysisResult.Adult.IsRacyContent || analysisResult.Adult.IsGoryContent);
    }

    private void OnBackClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage();
    }
}
