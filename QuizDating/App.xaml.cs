using QuizDating.MVVM.Views;
using QuizDating.Data;

namespace QuizDating
{
    public partial class App : Application
    {
        public static LocalDbService _db { get; set; }
        public App(LocalDbService db)
        {
            InitializeComponent();

            _db = db;
            MainPage = new LoginPage(_db);
        }
    }
}