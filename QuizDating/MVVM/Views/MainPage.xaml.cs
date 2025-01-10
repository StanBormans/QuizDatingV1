using QuizDating.Data;

namespace QuizDating.MVVM.Views
{
    public partial class MainPage : ContentPage
    {
        public LocalDbService LocalDbService { get; set; } =  new LocalDbService();
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
            Application.Current.MainPage = new LoginPage(LocalDbService);
        }
    }
}
