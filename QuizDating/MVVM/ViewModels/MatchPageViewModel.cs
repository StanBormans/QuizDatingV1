using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizDating.Models;
using QuizDating.MVVM.Views;

namespace QuizDating.MVVM.ViewModels
{
    public partial class MatchPageViewModel : ObservableObject
    {
        private List<User> users;
        private int currentIndex = 0;

        [ObservableProperty]
        private string profilePicture;

        [ObservableProperty]
        private string userName;

        public MatchPageViewModel()
        {
            users = new List<User>
            {
                new User { Id = 1, UserName = "Alice", ProfilePicture = "alice.jpg" },
                new User { Id = 2, UserName = "Bob", ProfilePicture = "bob.jpg" },
                new User { Id = 3, UserName = "Carol", ProfilePicture = "carol.jpg" }
            };

            LoadCurrentUser();
        }

        [RelayCommand]
        public void Like()
        {
            HandleSwipe(true);
        }

        [RelayCommand]
        public void Dislike()
        {
            HandleSwipe(false);
        }

        private void LoadCurrentUser()
        {
            if (currentIndex < users.Count)
            {
                var currentUser = users[currentIndex];
                ProfilePicture = currentUser.ProfilePicture;
                UserName = currentUser.UserName;
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
}
