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
        public object validateCurrencies(ExchangeRateRequest currency)
        {
            var currencies = new[] { "DOP", "USD", "EUR" };

            if (currency?.SourceCurrency == null || currency?.TargetCurrency == null)
            {
                return "Moneda no válida";
            }

            if (currency.SourceCurrency == currency.TargetCurrency)
            {
                return "Monedas iguales";
            }

            if (!currencies.Contains(currency.SourceCurrency.ToUpper()) || !currencies.Contains(currency.TargetCurrency.ToUpper()))
            {
                return null;
            }

            return true;

        }
}
}
