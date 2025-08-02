namespace ExchangeRateWebApi.ViewModel
{
    public class ExchangeRateDTO
    {
        public string SourceCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal Amount { get; set; }
    }
}
