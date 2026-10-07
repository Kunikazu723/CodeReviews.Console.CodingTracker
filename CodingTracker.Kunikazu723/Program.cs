using CodingTracker.Kunikazu723.Dao;
using CodingTracker.Kunikazu723.Models;
using CodingTracker.Kunikazu723.Services;
using CodingTracker.Kunikazu723.UserLand;
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

            var validation = new Validation(dateTimeFormat);
            var userInput = new UserInput(validation);
            IDao<CodingSession> codingSessionDao = new CodingSessionDao();
            var codeSessionService = new CodeSessionService(codingSessionDao, userInput);
            var userInterface = new UserInterface(userInput, codeSessionService);
            userInterface.MainMenu();
        }
    }
}
