using Microsoft.Extensions.Configuration;
namespace CodingTracker.Kunikazu723
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string databasePath = configuration["DatabasePath"] 
                ?? throw new InvalidOperationException("DatabasePath is missing from appsettings.json");

            string connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection String not found in appsettings.json");

            string dateTimeFormat = configuration["DateTimeFormat"]
                ?? throw new InvalidOperationException("DateTime Format not found in appsettings.json");

            var userInterface = new UserInterface();
            userInterface.MainMenu();
        }
    }
}
