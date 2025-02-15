using Lab1.Model;
using Lab1.Services;
using Microsoft.Extensions.Logging;

namespace Lab1
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

            builder.Services.AddSingleton<Page1>();
            builder.Services.AddSingleton<Page2>();
            builder.Services.AddSingleton<Page3>();
            builder.Services.AddSingleton<Page4>();
            builder.Services.AddSingleton<ProgressIndicator>();
            builder.Services.AddTransient<IDbService, SQLiteService>(provider =>
            {
                var service = new SQLiteService();
                service.Init();
                return service;
            });
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IRateService, RateService>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
