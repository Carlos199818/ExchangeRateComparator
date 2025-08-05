using ExchangeRateComparator;
using ExchangeRateComparator.Helpers;
using ExchangeRateComparator.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Net.Http;

class Program
{
    static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try
        {
            var services = new ServiceCollection();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog();
            });

            services.AddSingleton<HttpClient>();
            services.AddSingleton<IExchangeService, ExchangeOneService>();
            services.AddSingleton<ExchangeOneService>();
            services.AddSingleton<ExchangeTwoService>();
            services.AddSingleton<ExchangeThreeService>();
            services.AddSingleton<UserInputApp>();

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILogger<ExchangeRateComparer>>();

            var comparer = new ExchangeRateComparer(
                new ExchangeOneService(serviceProvider.GetRequiredService<HttpClient>()),
                new ExchangeTwoService(serviceProvider.GetRequiredService<HttpClient>()),
                new ExchangeThreeService(serviceProvider.GetRequiredService<HttpClient>()),
                new UserInputApp(),
                logger
            );

            while (true)
            {
                await comparer.RunAsync();

                Console.WriteLine("\n¿Deseas hacer otra comparación? (S/N): ");
                var input = Console.ReadLine()?.Trim().ToUpper();

                if (input != "S")
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "La aplicación falló de forma inesperada");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}