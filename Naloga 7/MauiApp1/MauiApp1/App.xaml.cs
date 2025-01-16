using MauiApp1.Services;

namespace MauiApp1
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; } // Property to access the service provider

        public App(IServiceProvider serviceProvider)
        {
            Services = serviceProvider; // Assign the service provider to the property

            try
            {
                InitializeComponent();

                var voznikService = serviceProvider.GetRequiredService<VoznikService>();
                var ekipaService = serviceProvider.GetRequiredService<EkipaService>();
                var voznikVEkipiService = serviceProvider.GetRequiredService<VoznikVEkipiService>();

                MainPage = new NavigationPage(new MainPage(voznikService, ekipaService, voznikVEkipiService)); // Pass the services to MainPage
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Napaka: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }
    }
}
