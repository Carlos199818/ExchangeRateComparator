using ExchangeRateComparator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateComparator.Services
{
    public interface IExchangeService
    {
        Task<decimal?> GetExchangeAsync(ExchangeRateRequest request, string url);
    }
}
