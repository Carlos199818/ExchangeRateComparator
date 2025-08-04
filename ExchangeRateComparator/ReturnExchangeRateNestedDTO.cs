namespace ExchangeRateWebApi.ViewModel
{
    public class ReturnExchangeRateNestedDTO
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public DataPayload? Data { get; set; } = new();

        public class DataPayload
        {
            public decimal Total { get; set; }
        }
    }
}
