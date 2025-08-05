using ExchangeRateComparator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateComparator.Helpers
{
    public class validCurrency()
    {
        public object validateCurrencies(string currency)
        {
            var currencies = new[] { "DOP", "USD", "EUR" };

            if (!currencies.Contains(currency.ToUpper()) || !currencies.Contains(currency.ToUpper()))
            {
                return null;
            }

            return true;

        }
}
}
