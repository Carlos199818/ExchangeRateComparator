using System.Text.Json.Serialization;

public class ExchangeRateNestedDTO
{
    [JsonPropertyName("exchange")]
    public InnerExchange Exchange { get; set; } = new();

    public class InnerExchange
    {
        [JsonPropertyName("sourceCurrency")]
        public string SourceCurrency { get; set; } = string.Empty;

        [JsonPropertyName("targetCurrency")]
        public string TargetCurrency { get; set; } = string.Empty;

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
    }
}