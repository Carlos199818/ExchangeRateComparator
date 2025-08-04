using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateComparator.Models.ViewModel
{
    public class ReturnNestedDTO
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
