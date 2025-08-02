using ExchangeRateComparator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateComparator.Helpers
{
    public class UserInputApp
    {
        public ExchangeRateRequest GetExchangeRateRequest() 
        {
            Console.WriteLine("=== Comparador de Tasa de Cambio ===\n");

            Console.Write("Moneda origen (ej. USD): ");
            var sourceCurrency = Console.ReadLine()?.Trim().ToUpper();

            Console.Write("Moneda destino (ej. DOP): ");
            var targetCurrency = Console.ReadLine()?.Trim().ToUpper();

            Console.Write("Monto a convertir: ");
            var amountInput = Console.ReadLine();

            if (!decimal.TryParse(amountInput, out decimal amount))
            {
                Console.WriteLine("Error: Monto inválido.");
                return null!;
            }

            return new ExchangeRateRequest
            {
                SourceCurrency = sourceCurrency!,
                TargetCurrency = targetCurrency!,
                Amount = amount
            };
        }
    }
}
