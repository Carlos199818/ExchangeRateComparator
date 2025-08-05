using ExchangeRateComparator.Models;
using ExchangeRateComparator.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ExchangeRateComparator.Services
{
    public class ExchangeTwoService : IExchangeService
    {
        private readonly HttpClient _httpClient;

        public ExchangeTwoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal?> GetExchangeAsync(ExchangeRateRequest dto, string url)
        {
            var endpoint = $"{url}exchange-two";
            var xmlRequest = new XmlDTO
            {
                SourceCurrency = dto.SourceCurrency,
                TargetCurrency = dto.TargetCurrency,
                Amount = dto.Amount
            };

            var xmlSerializer = new XmlSerializer(typeof(XmlDTO));
            using var ms = new MemoryStream();
            xmlSerializer.Serialize(ms, xmlRequest);
            ms.Position = 0;

            var content = new StringContent(Encoding.UTF8.GetString(ms.ToArray()), Encoding.UTF8, "application/xml");

            try
            {
                var response = await _httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                var xmlResponseSerializer = new XmlSerializer(typeof(ReturnXmlDTO));
                var result = (ReturnXmlDTO?)xmlResponseSerializer.Deserialize(stream);
                return result?.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en TWO: {ex.Message}");
                return null;
            }
        }
    }
}