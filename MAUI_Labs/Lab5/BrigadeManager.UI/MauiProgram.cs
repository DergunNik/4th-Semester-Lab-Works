using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using BrigadeManager.Persistence;
using BrigadeManager.Application;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using BrigadeManager.Persistence.Data;
using Microsoft.EntityFrameworkCore;


namespace BrigadeManager.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            string settingsStream = "BrigadeManager.UI.appsettings.json";

            var builder = MauiApp.CreateBuilder();

            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream(settingsStream);
            builder.Configuration.AddJsonStream(stream);

            var connStr = builder.Configuration
                .GetConnectionString("SqliteConnection");
            string dataDirectory = FileSystem.Current.AppDataDirectory + "/";

            connStr = String.Format(connStr, dataDirectory);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                   .UseSqlite(connStr)
                   .Options;

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services
                .AddApplication()
                .AddPersistence(options)
                .RegisterPages()
                .RegisterViewModels();

            DbInitializer
                .Initialize(builder.Services.BuildServiceProvider())
                .Wait();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            Console.WriteLine("MauiApp created");
            return builder.Build();
        }
    }
}
