using ExchangeRateComparator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ExchangeRateComparator.Services;
using ExchangeRateComparator.Helpers;
using Microsoft.Extensions.Logging;

namespace ExchangeRateComparator
{
    public class ExchangeRateComparer
    {
        private readonly IExchangeService _serviceOne;
        private readonly IExchangeService _serviceTwo;
        private readonly IExchangeService _serviceThree;
        private readonly UserInputApp _userInputApp;
        private readonly ILogger<ExchangeRateComparer> _logger;


        public ExchangeRateComparer(
            IExchangeService serviceOne, 
            IExchangeService serviceTwo, 
            IExchangeService serviceThree, 
            UserInputApp userInputApp, 
            ILogger<ExchangeRateComparer> logger)
        {
            _serviceOne = serviceOne;
            _serviceTwo = serviceTwo;
            _serviceThree = serviceThree;
            _userInputApp = userInputApp;
            _logger = logger;
        }

        decimal TruncateDecimal(decimal value, int decimals)
{
    decimal factor = (decimal)Math.Pow(10, decimals);
    return Math.Truncate(value * factor) / factor;
}

        public async Task RunAsync()
        {
            var baseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") ?? "http://localhost:5039";

            var request = _userInputApp.GetExchangeRateRequest();

            var validCurrencies = new validCurrency().validateCurrencies;

            if(validCurrencies(request) == null)
            {
                Console.WriteLine("Moneda no disponible.");
                return;
            }

            if (request == null) return; 

            _logger.LogInformation("Iniciando comparación de tasas para {Amount} {SourceCurrency} -> {TargetCurrency}", 
                request.Amount, request.SourceCurrency, request.TargetCurrency);

            if (string.IsNullOrEmpty(baseUrl))
            {
                Console.WriteLine("La variable de entorno API_BASE_URL no está configurada.");
                return;
            }

            _logger.LogInformation($"Usando API_BASE_URL: {baseUrl}");

            var results = new List<(string Api, decimal? Result)>
            {
                ("ONE", await _serviceOne.GetExchangeAsync(request, baseUrl)),
                ("TWO", await _serviceTwo.GetExchangeAsync(request, baseUrl)),
                ("THREE", await _serviceThree.GetExchangeAsync(request, baseUrl))
            };

            var best = results
                .Where(r => r.Result.HasValue)
                .OrderByDescending(r => r.Result.Value)
                .FirstOrDefault();
            
            foreach (var result in results)
            {
                if (!result.Result.HasValue)
                {
                    Console.WriteLine("No fue posible conseguir la tasa");
                    _logger.LogWarning("No se pudo obtener ninguna tasa de cambio.");

                }
                Console.WriteLine($"{result.Api}-EXCHANGE ofrece: {result.Result:F3}");
            }

            if (best.Result.HasValue)
            {
                Console.WriteLine($"\nLa mejor oferta es de {best.Api} con una tasa de {best.Result.Value:F2} lo que le hace un total de: {(best.Result.Value * request.Amount):F2}");
            }
            else
            {
                Console.WriteLine("\nNo se pudo obtener ninguna tasa.");
                _logger.LogWarning("No se pudo obtener ninguna tasa.");
            }
        }
    }
}
