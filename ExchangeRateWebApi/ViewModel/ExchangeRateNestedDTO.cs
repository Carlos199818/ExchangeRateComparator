using System.Text.Json.Serialization;

public class ExchangeRateNestedDTO
{
    [JsonPropertyName("exchange")]
    public InnerExchange Exchange { get; set; }

    public class InnerExchange
    {
        [JsonPropertyName("sourceCurrency")]
        public string SourceCurrency { get; set; }

        [JsonPropertyName("targetCurrency")]
        public string TargetCurrency { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
    }
}