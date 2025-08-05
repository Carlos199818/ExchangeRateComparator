using ExchangeRateComparator.Models;
using ExchangeRateComparator.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExchangeRateComparator.Services
{
    public class ExchangeThreeService : IExchangeService
    {
        private readonly HttpClient _httpClient;

        public ExchangeThreeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal?> GetExchangeAsync(ExchangeRateRequest dto, string url)
        {
            var endpoint = $"{url}exchange-three";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var body = JsonSerializer.Serialize(new
            {
                exchange = new
                {
                    sourceCurrency = dto.SourceCurrency,
                    targetCurrency = dto.TargetCurrency,
                    amount = dto.Amount
                }
            }, options);

            var content = new StringContent(body, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                var bodyText = await response.Content.ReadAsStringAsync();
                var parsed = JsonSerializer.Deserialize<ReturnNestedDTO>(bodyText, options);

                return parsed?.Data?.Total;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en THREE: {ex.Message}");
                return null;
            }
        }
    }
}