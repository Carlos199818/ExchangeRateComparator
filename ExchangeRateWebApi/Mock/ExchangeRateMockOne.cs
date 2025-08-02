using ExchangeRateWebApi.ViewModel;

namespace ExchangeRateWebApi.Mock
{
    public class ExchangeRateMockOne
    {
        public static readonly Dictionary<(string, string), decimal> Rates = new()
        {
            { ("DOP", "USD"), 0.016m },
            { ("DOP", "EUR"), 0.017m },
            { ("USD", "DOP"), 60.75m },
            { ("USD", "EUR"), 0.86m },
            { ("EUR", "DOP"), 70.36m },
            { ("EUR", "USD"), 1.16m },
        };

        public static decimal? GetRateOne(ExchangeRateDTO exchangeRate)
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
