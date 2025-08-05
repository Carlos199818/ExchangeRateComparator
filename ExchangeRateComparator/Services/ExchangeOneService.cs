using ExchangeRateComparator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExchangeRateComparator.Services
{
    public class ExchangeOneService : IExchangeService
    {
        private readonly HttpClient _httpClient;

        public ExchangeOneService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<decimal?> GetExchangeAsync(ExchangeRateRequest dto, string url)
        {
            var endpoint = $"{url}exchange-one";
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<Dictionary<string, decimal>>(body);
                return data?["rate"];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ONE: {ex.Message}");
                return null;
            }
        }
    }
}