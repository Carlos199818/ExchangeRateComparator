using ExchangeRateWebApi.ViewModel;

namespace ExchangeRateWebApi.Mock
{
    public class ExchangeRateMockThree
    {
        public static readonly Dictionary<(string, string), decimal> Rates = new()
        {
            { ("DOP", "USD"), 0.015m },
            { ("DOP", "EUR"), 0.014m },
            { ("USD", "DOP"), 61.87m },
            { ("USD", "EUR"), 0.88m },
            { ("EUR", "DOP"), 69.47m },
            { ("EUR", "USD"), 1.45m },
        };

        public static decimal? GetRateThree(ExchangeRateDTO exchangeRate)
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
