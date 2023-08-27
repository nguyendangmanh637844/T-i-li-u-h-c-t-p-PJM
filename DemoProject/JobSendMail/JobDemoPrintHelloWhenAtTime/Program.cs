using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackgroundJobExample
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<SendMail>();
                });

            await hostBuilder.RunConsoleAsync();
        }
    }
}