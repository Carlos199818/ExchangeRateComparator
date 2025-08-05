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

        public async Task RunAsync()
        {
            var baseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") ?? "http://localhost:5039";

            var request = _userInputApp.GetExchangeRateRequest();

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

            foreach (var result in results)
            {
                _logger.LogInformation("Resultado de {Api}: {Result}", result.Api, result.Result?.ToString("0.00") ?? "Error");
            }

            var best = results
                .Where(r => r.Result.HasValue)
                .OrderByDescending(r => r.Result.Value)
                .FirstOrDefault();
            
            if (best.Result.HasValue)
            {
                _logger.LogInformation("La mejor oferta es de {Api} con una tasa de {Rate}", best.Api, best.Result.Value);
            }
            else
            {
                _logger.LogWarning("No se pudo obtener ninguna tasa de cambio.");
            }
            
            foreach (var result in results)
            {
                Console.WriteLine($"{result.Api}: {(result.Result?.ToString("0.00") ?? "No fue posible conseguir la tasa")}");
            }

            if (best.Result.HasValue)
            {
                Console.WriteLine($"\nLa mejor oferta es de {best.Api} con una tasa de {best.Result.Value} lo que le hace un total de: {best.Result.Value * request.Amount}");
            }
            else
            {
                Console.WriteLine("\nNo se pudo obtener ninguna tasa.");
            }
        }
    }
}
