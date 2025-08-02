namespace ExchangeRateWebApi.ViewModel
{
    public class ReturnExchangeRateNestedDTO
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public DataPayload Data { get; set; }

        public class DataPayload
        {
            public decimal Total { get; set; }
        }
    }
}
