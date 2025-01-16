using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using MauiApp1.Services;
using System;
using System.Net.Http;

namespace MauiApp1
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

            // Register HttpClient with the base address
            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7253/") // Your API base URL
            });

            // Register the services
            builder.Services.AddSingleton<VoznikService>();
            builder.Services.AddSingleton<EkipaService>();
            builder.Services.AddSingleton<VoznikVEkipiService>();

            return builder.Build();
        }
    }
}
