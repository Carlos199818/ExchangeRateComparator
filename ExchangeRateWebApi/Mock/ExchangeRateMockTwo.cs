using ExchangeRateWebApi.ViewModel;

namespace ExchangeRateWebApi.Mock
{
    public class ExchangeRateMockTwo
    {
        public static readonly Dictionary<(string, string), decimal> Rates = new()
        {
            { ("DOP", "USD"), 0.017m },
            { ("DOP", "EUR"), 0.015m },
            { ("USD", "DOP"), 61.75m },
            { ("USD", "EUR"), 0.91m },
            { ("EUR", "DOP"), 70.19m },
            { ("EUR", "USD"), 1.03m },
        };

        public static decimal? GetRateTwo(ExchangeRateDTO exchangeRate)
        {
            var key = (exchangeRate.SourceCurrency.ToUpper(), exchangeRate.TargetCurrency.ToUpper());
            if (Rates.TryGetValue(key, out var rates))
            {
                return rates;
            }

            return null;
        }
    }
}
