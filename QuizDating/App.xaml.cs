using QuizDating.MVVM.Views;

namespace QuizDating
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }
    }
}