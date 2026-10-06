using Microsoft.Extensions.Configuration;
namespace CodingTracker.Kunikazu723
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                

        }
    }
}
