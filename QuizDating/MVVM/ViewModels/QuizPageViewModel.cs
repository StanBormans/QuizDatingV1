using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizDating.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizDating.Models;

namespace QuizDating.MVVM.ViewModels
{
    public partial class QuizPageViewModel : ObservableObject
    {

        [ObservableProperty]
        private List<Quiz> quizzes;

    }
}
