namespace QuizDating.MVVM.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void GoToQuiz(object sender, EventArgs e)
        {
            Application.Current.MainPage = new QuizPage();
        }

        public void GoToMatch(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MatchPage();
        }

        public void GoToLogin(object sender, EventArgs e)
        {
            //var loginPage = MauiProgram.CreateMauiApp().Services.GetRequiredService<LoginPage>();
            //Application.Current.MainPage = loginPage;
        }
    }
}
