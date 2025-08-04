namespace ExchangeRateWebApi.Models
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public string SourceCurrency { get; set; } = string.Empty;
        public string TargetCurrency { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
