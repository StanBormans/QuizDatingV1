using Microsoft.Extensions.Logging;
using QuizDating.MVVM.ViewModels;
using QuizDating.MVVM.Views;

namespace QuizDating
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<LocalDbService>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<MatchPageViewModel>();
            builder.Services.AddTransient<QuizPageViewModel>();

            return builder.Build();
        }
    }
}
