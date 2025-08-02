namespace ExchangeRateWebApi.Models
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public string SourceCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal Amount { get; set; }
    }
}
